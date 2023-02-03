using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemSheetAnswer.Models;

namespace ProblemSheetAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerController : ControllerBase
    {
        private readonly UserLawyerDbContext _context;
        public LawyerController(UserLawyerDbContext context) 
        {
            _context = context;
        }

        [HttpPost("LawyerRegistration")]
        [Authorize]
        public IActionResult LawyerRegistration([FromForm] string name, string email, int yearOfExp)
        {
            var lawyer = new Lawyer();
            lawyer.Name = name;
            lawyer.Email = email;
            lawyer.Experience = yearOfExp;

            _context.Lawyers.Add(lawyer);
            _context.SaveChanges();
            return Ok(lawyer);
        }

        [HttpGet("ShowProfile")]
        [Authorize]
        public IActionResult ShowProfile(int Id)
        {
            var lawyer = _context.Lawyers.Find(Id);
            return Ok(lawyer);
        }
    }
}
