//using GraphQL;
//using GraphQL.Types;
//using MediatR;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using ProjectBank.Application.Features.Customers.Commands;
//using ProjectBank.Infrastructure.Entities;
//using ProjectBank.Presentation.GraphQL.Models;
//using System.Threading.Tasks;

//namespace ProjectBank.Presentation.GraphQL.Mutations
//{
//    public class CustomerMutation : ObjectGraphType
//    {
//        public CustomerMutation(IMediator _mediator)
//        {
//            Name = "Mutation";

//            Field<CustomerType>("addCustomer")
//                .Arguments(new QueryArguments(
//                    new QueryArgument<NonNullGraphType<CustomerInputType>> { Name = "customer" }
//                ))
//                .ResolveAsync(async context =>
//                {
//                    var customer = context.GetArgument<Customer>("customer");
//                    CreateCustomerCommand customerCommand = new CreateCustomerCommand() { 
//                        Name = customer.Name,
//                        LastName = customer.LastName,
//                        Country = customer.Country,
//                        Email = customer.Email,
//                        Phone = customer.Phone,
//                    };
//                    return await _mediator.Send(customerCommand);
//                });
//        }
//    }
//}