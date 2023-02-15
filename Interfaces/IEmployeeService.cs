using DotNetAPITutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAPITutorial.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployee();
        Employee GetEmployee(int id);
        bool IsEmployeeExists(int id);
        bool SaveEmployee(int loginId, Employee employee);
    }
}
