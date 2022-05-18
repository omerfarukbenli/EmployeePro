using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _context;
        public LoginController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var userdetails = _context.UserModels.AsQueryable();
            return Ok();
        }
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] UserModel userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            else
            {
                _context.UserModels.Add(userObj);
                _context.SaveChanges();
                return Ok(
                    new
                    {
                        StatusCode = 200,
                        message = "kullanıcı ekleme başarılı"
                    }
                    );
            }


        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            else
            {
                var user = _context.UserModels.Where(a =>
                a.UserName == userObj.UserName && a.Password == userObj.Password)
                      .FirstOrDefault();
                if (user != null)
                {
                    return Ok(
                        new
                        {
                            StatusCode = 200,
                            message = "giriş başarılı",
                            
                        }
                        );
                }
                else
                {
                    return NotFound(
                        new
                        {
                            StatusCode = 404,
                            message = "kullanıcı bulunamadı"
                        }
                        );
                }
            }
        }
    }
}
