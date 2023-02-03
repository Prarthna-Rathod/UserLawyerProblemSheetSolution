using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProblemSheetAnswer.Models;

namespace ProblemSheetAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly UserLawyerDbContext _context;

        public QuestionController(UserLawyerDbContext context)
        {
            _context = context;
        }

        private string MediaUpload(IFormFile file)
        {
            var x = Guid.NewGuid().ToString();
            var filepath = Path.Combine(Directory.GetCurrentDirectory(),
               @"MediaUploads", x + "-" + file.FileName);

            using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return filepath;
        }

        [HttpPost("AskQuestion")]
        [Authorize]
        public IActionResult AskQuestion([FromForm] string question, IFormFile MediaDetails, int UserId, bool IsAssign,int? LawyerId)
        {
            var que = new Questions();
            que.Description = question;

            var file = que.MediaDetails = MediaDetails;
            string path = MediaUpload(file);
            que.MediaFile = path;

            que.UserId = UserId;
            que.User = _context.Users.Find(UserId);
            que.Assign = IsAssign;


            if(IsAssign == true)
            {
                que.AssignTo = LawyerId;
                que.AssignToLawyer = _context.Lawyers.Find(LawyerId);
                que.Picked = false;
            }
            else
            {
                que.Picked = false;
            }

            _context.questions.Add(que);
            _context.SaveChanges();
            return Ok("Question Submit Successfull");
        }

        [HttpGet("ShowQuestionList")]
        [Authorize]
        public IActionResult ShowQuestionList()
        {
            var queList = from q in _context.questions
                          where (q.Assign == false) && (q.Picked == false)
                          select new { q.Id, q.Description, q.MediaFile, q.User.Name } ;

            return Ok(queList);
        }

        [HttpGet("ShowQuestionDetails")]
        [Authorize]
        public IActionResult ShowQuestionDetails(int id)
        {
            var quedata = from q in _context.questions
                          where q.Id == id
                          select new
                          {
                              q.Id,
                              q.Description,
                              q.User.Name
                          };

            var que = _context.questions.Find(id);
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),que.MediaFile.ToString());
            
            if (System.IO.File.Exists(path))
            {
                byte[] b = System.IO.File.ReadAllBytes(path);
                //img = File(b, "image/png");
                return File(b,"image/png");
            }
            return Ok(quedata);
            
        }

        [HttpGet("QuesAssignToLawyer")]
        [Authorize]
        public IActionResult QuesAssignToLawyer(string emailid)
        {
            int lawyer = _context.Lawyers.FirstOrDefault(x => x.Email.ToLower() == emailid.ToLower()).Id;

            var data = from q in _context.questions
                       where q.AssignTo == lawyer
                       select new
                       {
                           q.Id,
                           q.Description,
                           q.User.Name,
                           q.Assign,
                           q.AssignToLawyer,
                           q.Picked
                       };

            return Ok(data);
        }

        [HttpPut("GiveAnswer")]
        [Authorize]
        public IActionResult GiveAnswer([FromForm] int queId,string answer,int lawyerId)
        {
            var que = _context.questions.Find(queId);
            if(que.Assign == false)
            {
                que.LawyerId = lawyerId;
                que.Lawyer = _context.Lawyers.Find(lawyerId);
                que.Picked = true;
                que.PickedBy = lawyerId;
                que.PickedByLawyer = que.Lawyer;
                que.Answer = answer;

                _context.Entry(que).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok("Answer submitted successfully");
            }
            if(que.Assign == true && que.AssignTo == lawyerId)
            {
                que.LawyerId = lawyerId;
                que.Lawyer = _context.Lawyers.Find(lawyerId);
                que.Picked = true;
                que.PickedBy = lawyerId;
                que.PickedByLawyer = que.Lawyer;
                que.Answer = answer;

                _context.Entry(que).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok();
                
            }

            return BadRequest("This question is Assigned you can't give answer");
        }

        [HttpGet("ShowAnswerConversations")]
        [Authorize]
        public IActionResult ShowAnswerConversations(int UserId,bool AssignedorNot)
        {
            if(AssignedorNot == false)
            {
                var answer = from ans in _context.questions 
                             where ans.UserId == UserId && ans.Picked == true 
                             && ans.Answer != null && ans.Assign==false 
                             select new
                             {
                                 ans.Id,
                                 ans.Description,
                                 ans.Answer,
                                // ans.Picked,
                                 ans.PickedByLawyer
                                 
                             };
                if (answer != null)
                { return Ok(answer); }
                else
                    return BadRequest("Question is not Picked by any Lawyer");
            }

            if(AssignedorNot == true)
            {
                var ans = from a in _context.questions
                          //join old in _context.oldConversations
                          //   on a.Id equals old.QuesId
                          where a.UserId == UserId && a.Assign == true && a.AssignTo == a.LawyerId
                          && a.Answer != null 
                          // || a.Id == old.QuesId
                          select new { 
                          a.Id,
                          a.Description,
                          a.Answer,
                          //a.Assign,
                          //a.AssignToLawyer,
                          //a.PickedByLawyer,
                         // old.Question
                          };

                if (ans != null)
                { return Ok(ans); }
                else
                    return BadRequest("Question is not Picked by any Lawyer");
            }
            return Ok("Not found");
        }

        [HttpPut("AnswerSatisfaction")]
        [Authorize]
        public IActionResult AnswerSatisfaction([FromForm] int userId, int questionId, bool IsSatisfy)
        {
            var question = _context.questions.Find(questionId);
            if(question.UserId == userId && question.Answer!=null)
            {
                question.IsUserSatisfied = IsSatisfy;

                _context.Entry(question).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok("Successfull");
            }
            
            return BadRequest("Question is not picked yet!! So you can't give Satisfaction");
        }

        [HttpPost("ReAssignQuestion"),HttpPut]
        [Authorize]
        
        public IActionResult ReAssignQuestion(int questionId)
        {
            var ques = _context.questions.Find(questionId);
            
            if(ques.IsUserSatisfied == false)
            {
                var oc = new OldConversation();
                oc.QuesId = questionId;
                oc.Question = ques.Description;
                oc.Answer = ques.Answer;
                oc.solvedBy = (int)ques.LawyerId;
                _context.oldConversations.Add(oc);
                _context.SaveChanges();

                var que = _context.questions.Find(questionId);
                que.Answer = null;
                que.LawyerId = null;
                que.Lawyer = null;
                que.Picked = false;
                que.PickedBy = null;
                que.PickedByLawyer = null;
                que.IsUserSatisfied = null;

                _context.questions.Update(que);
                //_context.Entry(que).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok("Re-Assigned Successfully");
            }

            return BadRequest("Can't Re-Assigned !! User is already Satisfied.");
        }

        [HttpGet("AllConversations")]
        [Authorize]
        public IActionResult AllConversations(int Queid)
        {
            var data = from que in _context.questions
                       join old in _context.oldConversations
                       on que.Id equals old.QuesId
                       where que.Id == Queid || old.QuesId == Queid
                       select new
                       { 
                            que.Id,
                            que.Description,
                            que.Answer,
                            old
                       };
            if (data != null)
                return Ok(data);
            else
                return BadRequest("No old record found");
        }

    }
}
