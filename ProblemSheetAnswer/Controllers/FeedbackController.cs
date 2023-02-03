using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProblemSheetAnswer.Models;

namespace ProblemSheetAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly UserLawyerDbContext _context;

        public FeedbackController(UserLawyerDbContext context)
        {
            _context = context;
        }

        [HttpPost("RateToLawyer"),HttpPut]
        [Authorize]
        public IActionResult RateToLawyer([FromForm] int lawyerId, string description,int rating)
        {
            var feedback = new Feedback();

            if(rating < 0 || rating > 5)
            {
                return BadRequest("Rating should be in between 0 to 5");
            }
            else
            {
                feedback.Description = description;
                feedback.LawyerId = lawyerId;
                feedback.Lawyer = _context.Lawyers.Find(lawyerId);
                feedback.Rating = rating;
                _context.feedbacks.Add(feedback);
                _context.SaveChanges();

                var lawyer = _context.Lawyers.Find(lawyerId);
                var x = from r in _context.feedbacks
                        where r.LawyerId == lawyerId
                        select r.Rating;
                var avg = x.Average();
                lawyer.AvgRate = avg;
                _context.Entry(lawyer).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok("Rating done successfully");
            }
            
        }
    }
}
