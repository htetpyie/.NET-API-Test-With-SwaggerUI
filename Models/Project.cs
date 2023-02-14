using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAPITutorial.Models
{
    public class Project : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectCode { get; set; }
        
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
