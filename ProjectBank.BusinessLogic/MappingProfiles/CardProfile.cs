using AutoMapper;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.MappingProfiles
{
    internal class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NumberCard, opt =>
                    opt.MapFrom(src => src.NumberCard))
                .ForMember(dest => dest.CardName, opt =>
                    opt.MapFrom(src => src.CardName))
                .ForMember(dest => dest.Pincode, opt =>
                    opt.MapFrom(src => src.Pincode))
                .ForMember(dest => dest.ExpirationDate, opt =>
                    opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.CVV, opt =>
                    opt.MapFrom(src => src.CVV))
                .ForMember(dest => dest.Balance, opt =>
                    opt.MapFrom(src => src.Balance))
                .ForMember(dest => dest.CurrencyCode, opt =>
                opt.MapFrom((src, dest, destMember, context) => (string)context.Items["currencyCode"]));
        }
    }
}
