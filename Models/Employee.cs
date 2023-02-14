using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAPITutorial.Models
{
    public class Employee : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StaffId { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public Department Department { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
