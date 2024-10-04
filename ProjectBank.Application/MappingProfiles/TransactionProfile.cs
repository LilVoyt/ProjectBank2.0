using AutoMapper;
using ProjectBank.BusinessLogic.Features.Accounts.Commands;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.MappingProfiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TransactionDate, opt =>
                opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Sum, opt =>
                opt.MapFrom(src => src.Sum))
                .ForMember(dest => dest.CardSender, opt =>
                opt.MapFrom(src => src.CardSender))
                .ForMember(dest => dest.CardReceiver, opt =>
                opt.MapFrom(src => src.CardReceiver));
        }

    }
}
