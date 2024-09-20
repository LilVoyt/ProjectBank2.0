using MediatR;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.DataAcces.Entities;
using System.Threading.Tasks;

namespace ProjectBank.Presentation.GraphQL.Mutations
{
    public class CustomerMutation
    {
        private readonly IMediator _mediator;

        public CustomerMutation(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Customer> AddCustomer(CreateCustomerCommand input)
        {
            return await _mediator.Send(input);
        }
    }
}
