using DotNetAPITutorial.Interfaces;
using DotNetAPITutorial.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAPITutorial.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _iEmployeeService;

        public EmployeeController(IEmployeeService iEmployee)
        {
            this._iEmployeeService = iEmployee;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employee = _iEmployeeService.GetAllEmployee();
            if ( !ModelState.IsValid )           
                return BadRequest(ModelState);          
            return Ok(employee);
        }

        [HttpGet("{empId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int empId)
        {           
            if (!_iEmployeeService.IsEmployeeExists(empId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var emp = _iEmployeeService.GetEmployee(empId);
            return Ok(emp);
        }
    }
}
