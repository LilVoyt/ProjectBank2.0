using AutoMapper;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.MappingProfiles
{
    public class CreditProfile : Profile
    {

        public CreditProfile()
        {
            CreateMap<Credit, CreditDto>()
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CardNumber, opt =>
                opt.MapFrom((src, dest, destMember, context) => context.Items["CardNumber"]))
                .ForMember(dest => dest.Principal, opt =>
                opt.MapFrom(src => src.Principal))
                .ForMember(dest => dest.AnnualInterestRate, opt =>
                opt.MapFrom(src => src.AnnualInterestRate))
                .ForMember(dest => dest.MonthlyPayment, opt =>
                opt.MapFrom(src => src.MonthlyPayment))
                .ForMember(dest => dest.StartDate, opt =>
                opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt =>
                opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.CurrencyName, opt =>
                opt.MapFrom((src, dest, destMember, context) => context.Items["CurrencyName"]))
                .ForMember(dest => dest.CreditTypeName, opt =>
                opt.MapFrom((src, dest, destMember, context) => context.Items["CreditTypeName"]));
        }
    }
}
