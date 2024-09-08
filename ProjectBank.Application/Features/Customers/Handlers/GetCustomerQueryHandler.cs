using FluentValidation;
using MediatR;
using ProjectBank.Application.Features.Customers.Queries;
using ProjectBank.Infrastructure.Entities;
using ProjectBank.Infrastructure.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Features.Customers.Handlers
{
    internal class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, List<Customer>>
    {
        private readonly ICustomerService _customerService;

        public GetCustomerQueryHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public Task<List<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var res = _customerService.Get(request.Search, request.SortItem, request.SortOrder);
            return res;
        }
    }
}
