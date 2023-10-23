using AutoMapper;
using DAL.Entities;
using MVCProject.Models;

namespace MVCProject.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
