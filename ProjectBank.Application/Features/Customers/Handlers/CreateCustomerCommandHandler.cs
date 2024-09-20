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
    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<Customer> _validator;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerService customerService, IValidator<Customer> validator, IMapper mapper)
        {
            _customerService = customerService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);

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
