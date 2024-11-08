using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace ProjectBank.BusinessLogic.Features.Customers.Handlers
{
    internal class CreateCustomerCommandHandler(ICustomerService customerService, IMapper mapper) : IRequestHandler<CreateCustomerCommand, Customer>
    {
        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = mapper.Map<Customer>(request);

            await customerService.Post(customer);
            return customer;

        }
    }
}
