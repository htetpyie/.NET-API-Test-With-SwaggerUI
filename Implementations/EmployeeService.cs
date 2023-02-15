using DotNetAPITutorial.Context;
using DotNetAPITutorial.Dto;
using DotNetAPITutorial.Interfaces;
using DotNetAPITutorial.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAPITutorial.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;

        public EmployeeService(DataContext context)
        {
            _context = context;
        }
       
        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employee.Where(e => e.IsDelete == false).OrderBy(e => e.Id).ToList();
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employee.Where(e => e.Id == id && e.IsDelete == false).FirstOrDefault();
        }

        public bool IsEmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id && e.IsDelete == false);
        }

        public bool SaveEmployee(int loginId, Employee employee)
        {
            employee.CreatedBy = loginId;
            _context.Add(employee);
            return  Save();
        }

        public bool UpdateEmployee(int loginId, Employee employee)
        {
            employee.UpdatedBy = loginId;
            _context.Update(employee);
            return Save();
        }

        private bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
