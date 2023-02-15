using AutoMapper;
using DotNetAPITutorial.Dto;
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
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService iEmployee, IMapper mapper)
        {
            this._iEmployeeService = iEmployee;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employee = _mapper.Map<List<EmployeeDTO>>(_iEmployeeService.GetAllEmployee());
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
            var emp = _mapper.Map<EmployeeDTO>(_iEmployeeService.GetEmployee(empId));
            return Ok(emp);
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromQuery]int userId,[FromBody] EmployeeDTO employee)
        {
            int loginUserId = userId;
            if (employee == null)
                return BadRequest(ModelState);

            var employeeByName = _iEmployeeService.GetAllEmployee()
                .Any(e => e.StaffId.TrimEnd() == employee.StaffId.TrimEnd());

            if (employeeByName)
            {
                ModelState.AddModelError("", "Employee Already Exists");// ("key","Employee Already Exists")
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeEntity = _mapper.Map<Employee>(employee);
            if (!_iEmployeeService.SaveEmployee(loginUserId, employeeEntity))
                return StatusCode(500, "Something went wrong while saving.");

            return Ok("successfully created.");               
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            int loginUserId = 1;
            if (employeeDTO == null)
                return BadRequest(ModelState);
            if (!_iEmployeeService.IsEmployeeExists(employeeDTO.Id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var employee = _mapper.Map<Employee>(employeeDTO);
            if (!_iEmployeeService.UpdateEmployee(loginUserId, employee))
            {
                ModelState.AddModelError("", "Something went wrong updating Employee");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
