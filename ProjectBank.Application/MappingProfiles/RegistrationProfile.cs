using AutoMapper;
using ProjectBank.BusinessLogic.Features.Accounts.Commands;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Register_Login.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.MappingProfiles
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<UserRegisterDto, CreateCustomerCommand>()
                .ForMember(dest => dest.FirstName, opt =>
                opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt =>
                opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Country, opt =>
                opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Email, opt =>
                opt.MapFrom(src => src.Email));

            CreateMap<UserRegisterDto, CreateAccountCommand>()
                .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Login, opt =>
                opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, opt =>
                opt.MapFrom(src => src.Password));

            CreateMap<CreateNewUserCommand, Customer>()
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.FirstName, opt =>
                opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt =>
                opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt =>
                opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt =>
                opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Country, opt =>
                opt.MapFrom(src => src.Country));

            CreateMap<CreateNewUserCommand, Account>()
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Name))
                //.ForMember(dest => dest.CustomerID, opt =>
                //opt.MapFrom(src => src.CustomerID))
                .ForMember(dest => dest.Login, opt =>
                opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, opt =>
                opt.MapFrom(src => src.Password));

            CreateMap<UserLoginDto, LoginIntoAccountCommand>()
                .ForMember(dest => dest.Login, opt =>
                opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, opt =>
                opt.MapFrom(src => src.Password));


        }
    }
}
