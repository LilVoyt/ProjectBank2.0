﻿using AutoMapper;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.MappingProfiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<AddCardCommand, Card>()
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.NumberCard, opt =>
                opt.MapFrom((src, dest, destMember, context) => (string)context.Items["creditCard"]))
                .ForMember(dest => dest.CardName, opt =>
                opt.MapFrom(src => src.CardName))
                .ForMember(dest => dest.Pincode, opt =>
                opt.MapFrom(src => src.Pincode))
                .ForMember(dest => dest.ExpirationDate, opt =>
                opt.MapFrom((src, dest, destMember, context) => (DateTime)context.Items["expirationDate"]))
                .ForMember(dest => dest.Balance, opt =>
                opt.MapFrom(src => 0))
                .ForMember(dest => dest.AccountID, opt =>
                opt.MapFrom(src => src.AccountID));
        }
    }
}
