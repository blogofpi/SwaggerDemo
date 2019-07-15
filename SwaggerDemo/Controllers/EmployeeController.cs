using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private List<Employee> employeeList;
        public EmployeeController()
        {
            employeeList = new List<Employee>
            {
                new Employee{Id = 1, FirstName="Jane", LastName="Doe", Age = 21, Title = "Developer"},
                new Employee{Id = 2, FirstName="Bob", LastName="Martin", Age = 21, Title = "Architect"}
            };
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        public ActionResult Get()
        {
            return Ok(employeeList);
        }

        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public ActionResult Get(int id)
        {
            var employee = employeeList.Where(x => x.Id == id).FirstOrDefault();
            if (employee == null)
            {
                return BadRequest(new ErrorResponse { ErrorCode = 404, ErrorMessage = "Employee Not Found" });
            }
            return Ok(employee);
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Title { get; set; }
    }

    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}