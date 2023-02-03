using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemSheetAnswer.Models;

namespace ProblemSheetAnswer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserLawyerDbContext _context;

        public UserController(UserLawyerDbContext context)
        {
            _context = context;
        }

        [HttpPost("UserRegistration")]
        [Authorize]
        public IActionResult UserRegistration([FromForm] string name,string email,long mobile)
        {
            var user = new User();
            user.Name = name;
            user.Email = email;
            user.Mobile = mobile;
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpGet("UserProfile")]
        [Authorize]
        public IActionResult UserProfile(int id)
        {
            var user = _context.Users.Find(id);
            return Ok(user);
        }

    }

    
}
