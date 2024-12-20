﻿using AutoMapper;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;

namespace ProjectBank.BusinessLogic.MappingProfiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<RegisterCommand, Customer>()
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

            CreateMap<RegisterCommand, Account>()
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Name, opt =>    
                opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CustomerID, opt =>
                opt.MapFrom((src, dest, destMember, context) => (Guid)context.Items["CustomerId"]))
                .ForMember(dest => dest.Login, opt =>
                opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, opt =>
                opt.MapFrom((src, dest, destMember, context) => (string)context.Items["HashedPassword"]))
                .ForMember(dest => dest.Role, opt =>
                opt.MapFrom(src => src.Role));
        }
    }
}
