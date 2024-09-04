using EmployeeServer.Data;
using EmployeeServer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly EmployeeContext _context;
        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = _context.Employees.ToList();
            return employees;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee(Employee employee)
        {

            if (employee == null)
            {
                return BadRequest("Employee object is null");
            }
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();

                return Ok("Employee added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("GetEmployeesByDept/{dept}")]
        public ActionResult<IEnumerable<Employee>> GetEmployee(string dept)
        {
            var employees = _context.Employees.Where(e => e.Dept == dept).ToList();
            if (employees == null)
            {
                return NotFound($"No employees found in the {dept} department");
            }

            return employees;
            //var employees = _context.GetEmployeesByDept(dept).Select(g => new
            //{
            //    g.Name
            //});
            //var emps=_context.GetEmployeesByDept(dept);
            //return emps;
        }

        [HttpGet]
        [Route("GetEmployeeCountByDept")]
        public ActionResult<IEnumerable<Employee>> GetEmployeeCount()
        {
            var employeeCount = _context.Employees.GroupBy(e => e.Dept)
                                                .Select(g => new
                                                {
                                                    dep = g.Key,
                                                    EmployeeCount = g.Count()
                                                }).ToList();
            return Ok(employeeCount);
        }

        [HttpGet]
        [Route("GetSalary")]
        public ActionResult<IEnumerable<Employee>> GetSalary()
        {
            var salaries = _context.Employees.Select(e => new
            {
                e.Salary
            });
            return Ok(salaries);
        }
    }
}
