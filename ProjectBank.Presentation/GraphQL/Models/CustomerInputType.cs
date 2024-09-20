using ProjectBank.Application.Features.Customers.Commands;
using ProjectBank.Infrastructure.Entities;

namespace ProjectBank.Presentation.GraphQL.Models
{
    public class CustomerInputType : InputObjectType<CreateCustomerCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateCustomerCommand> descriptor)
        {
            descriptor.Field(x => x.FirstName).Description("The first name of the customer.");
            descriptor.Field(x => x.LastName).Description("The last name of the customer.");
            descriptor.Field(x => x.Country).Description("The country of the customer.");
            descriptor.Field(x => x.PhoneNumber).Description("The phone number of the customer.");
            descriptor.Field(x => x.Email).Description("The email of the customer.");
        }
    }
}
