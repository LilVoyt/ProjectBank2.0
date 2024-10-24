﻿using AutoMapper;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
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

            CreateMap<CreateTransactionCommand, Transaction>()
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Date, opt =>
                opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Sum, opt =>
                opt.MapFrom(src => src.Sum))
                 .ForMember(dest => dest.Sum, opt =>
                opt.MapFrom(src => src.Sum))
                .ForMember(dest => dest.CardSenderID, opt =>
                opt.MapFrom((src, dest, destMember, context) => (Guid)context.Items["CardSenderId"]))
                .ForMember(dest => dest.CardReceiverID, opt =>
                opt.MapFrom((src, dest, destMember, context) => (Guid)context.Items["CardReceiverId"]))
                .ForMember(dest => dest.CurrencyId, opt =>
                opt.MapFrom((src, dest, destMember, context) => (Guid)context.Items["CurrencyId"]));
        }

    }
}
