using MediatR;
using ProjectBank.Application.Features.Customers.Queries;
using ProjectBank.Infrastructure.Entities;
using HotChocolate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBank.Presentation.GraphQL.Queries
{
    public class CustomerQuery
    {
        private readonly IMediator _mediator;

        public CustomerQuery(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<Customer>> Read()
        {
            var result = await _mediator.Send(new GetCustomerQuery());
            return result;
        }
    }
}
