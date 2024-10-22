using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Features.Accounts.Service;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Accounts.Handlers
{
    public class GetByIdQueryHandler(IAccountLogicService accountLogicService) 
        : IRequestHandler<GetByIdQuery, AccountDto>
    {
        public async Task<AccountDto> Handle([FromBody]GetByIdQuery request, CancellationToken cancellationToken)
        {
            var accountDto = await accountLogicService.GetDto(request);

            return accountDto;
        }

    }
}
