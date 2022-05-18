using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        public EmployeeController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("add_employee")]
        public IActionResult AddEmployee([FromBody] EmployeeModel employeeObj)
        {
            if (employeeObj == null)
            {
                return BadRequest();
            }
            else
            {
                /////dikkat
                _context.EmployeeModels.Add(employeeObj);
                _context.SaveChanges();
                return Ok(
                    new
                    {
                        StatusCode = 200,
                        Message = "işçi eklendi başarılı"
                    });
            }
        }
        [HttpPut("update_employe")]
        public IActionResult UpdateEmployee([FromBody] EmployeeModel employeeObj)
        {
            if (employeeObj == null)
            {
                return BadRequest();
            }
            var user = _context.EmployeeModels.AsNoTracking().FirstOrDefault(x => x.Id == employeeObj.Id);
            if (user == null)
            {
                return NotFound(
                    new
                    {
                        StatusCode = 404,
                        Message = "kullanıcı bulunamadı"
                    }
                    );
            }
            else
            {
                _context.Entry(employeeObj).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(
                    new
                    {
                        StatusCode = 200,
                        Message = "işçi güncelleme baaşrılı"

                    }
                    );
            }
        }
        [HttpDelete("delete-employee{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var user = _context.EmployeeModels.Find(id);
            if (user == null)
            {
                return NotFound(
                    new
                    {
                        StatusCode = 404,
                        Message = "kullanıcı bulunamadı"
                    }
                    );
            }
            else
            {
                _context.Remove(user);
                _context.SaveChanges();
                return Ok(
                    new
                    {
                        StatusCode = 200,
                        Message = "işçi silindi"
                    }
                    );
            }
        }
        [HttpGet("get_all_employees")]
        public IActionResult GetAllEmployess()
        {
            var employees = _context.EmployeeModels.AsQueryable();
            return Ok(
                new
                {
                    StatusCode = 200,
                    EmployeeDetails = employees
                }
                );
        }
        [HttpGet("get_employee/id")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _context.EmployeeModels.Find(id);
            if (employee == null)
            {
                return NotFound(
                    new
                    {
                        StatusCode = 404,
                        Message = "kullanıcı bulunmadı"
                    }
                    );
            }
            else
            {
                return Ok(
                    new
                    {
                        StatusCode = 200,
                        EmployeeDetails = employee
                    }
                    );
            }
        }
    }
}
