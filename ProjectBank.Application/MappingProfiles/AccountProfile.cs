using AutoMapper;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Customer, opt =>
                    opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Cards, opt =>
                    opt.MapFrom(src => src.Cards));

            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.FirstName, opt =>
                    opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt =>
                    opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Country, opt =>
                    opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt =>
                    opt.MapFrom(src => src.Email));

            CreateMap<Card, CardDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NumberCard, opt =>
                    opt.MapFrom(src => src.NumberCard))
                .ForMember(dest => dest.CardName, opt =>
                    opt.MapFrom(src => src.CardName))
                .ForMember(dest => dest.Pincode, opt =>
                    opt.MapFrom(src => src.Pincode))
                .ForMember(dest => dest.Data, opt =>
                    opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.CVV, opt =>
                    opt.MapFrom(src => src.CVV))
                .ForMember(dest => dest.Balance, opt =>
                    opt.MapFrom(src => src.Balance));
        }
    }
}