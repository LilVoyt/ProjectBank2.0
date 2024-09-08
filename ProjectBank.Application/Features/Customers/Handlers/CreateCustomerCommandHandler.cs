using FluentValidation;
using MediatR;
using ProjectBank.Application.Features.Customers.Commands;
using ProjectBank.Infrastructure.Entities;
using ProjectBank.Infrastructure.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace ProjectBank.Application.Features.Customers.Handlers
{
    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<Customer> _validator;

        public CreateCustomerCommandHandler(ICustomerService customerService, IValidator<Customer> validator)
        {
            _customerService = customerService;
            _validator = validator;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LastName = request.LastName,
                Country = request.Country,
                Phone = request.Phone,
                Email = request.Email
            };

            var validationResult = await _validator.ValidateAsync(customer, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            await _customerService.Post(customer);
            return customer;

        }
    }
}
