using AutoMapper;
using DotNetAPITutorial.Dto;
using DotNetAPITutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAPITutorial.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
