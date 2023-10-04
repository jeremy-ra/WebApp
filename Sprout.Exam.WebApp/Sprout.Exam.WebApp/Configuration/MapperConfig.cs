using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Entity.Entities;

namespace Sprout.Exam.WebApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
            CreateMap<Employee, EditEmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
