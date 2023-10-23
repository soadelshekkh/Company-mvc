using AutoMapper;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using MVCProject.Models;

namespace MVCProject.Mapper
{
	public class RegisterProfile : Profile
	{
		public RegisterProfile()
		{
            CreateMap<RegisterViewModel, UserRegister>().ReverseMap();
        }
	}
}
