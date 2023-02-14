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
    }
}
