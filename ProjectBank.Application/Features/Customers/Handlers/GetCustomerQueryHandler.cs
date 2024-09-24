using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Customers.Queries;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Customers.Handlers
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, List<Customer>>
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
