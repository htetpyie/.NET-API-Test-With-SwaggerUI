using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAPITutorial.Dto
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StaffId { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Something { get; set; }
    }
}
