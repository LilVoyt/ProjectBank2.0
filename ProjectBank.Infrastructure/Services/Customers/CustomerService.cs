using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Infrastructure.Data;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Infrastructure.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        private readonly IValidator<Customer> _validator;
        private readonly CustomerMapper _customerMapper;

        public CustomerService(DataContext context, IValidator<Customer> validator, CustomerMapper customerMapper)
        {
            _context = context;
            _validator = validator;
            _customerMapper = customerMapper;
        }

        public async Task<ActionResult<List<CustomerRequestModel>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<Customer> customers = _context.Customer;

            if (!string.IsNullOrEmpty(search))
            {
                customers = customers.Where(n => n.Name.ToLower().Contains(search.ToLower()));
            }

            Expression<Func<Customer, object>> selectorKey = sortItem?.ToLower() switch
            {
                "name" => customer => customer.Name,
                "lastname" => customer => customer.LastName,
                "country" => customer => customer.Country,
                "phone" => customer => customer.Phone,
                "email" => customer => customer.Email,
                _ => customers => customers.Name
            };

            customers = sortOrder?.ToLower() == "desc"
                ? customers.OrderByDescending(selectorKey)
                : customers.OrderBy(selectorKey);

            List<Customer> customerList = await customers.ToListAsync();

            List<CustomerRequestModel> response = _customerMapper.GetRequestModels(customerList);

            return response;
        }

        public async Task<Customer> Post(CustomerRequestModel customer)
        {
            var res = _customerMapper.GetCustomer(customer);

            var validationResult = await _validator.ValidateAsync(res);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            await _context.Customer.AddAsync(res);
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<Customer> Update(Guid id, CustomerRequestModel requestModel)
        {
            var account = await _context.Customer.FindAsync(id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            account = _customerMapper.PutRequestModelInCustomer(account, requestModel);
            var validationResult = await _validator.ValidateAsync(account);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
            _context.Customer.Update(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Customer> Delete(Guid id)
        {
            var account = await _context.Customer.FindAsync(id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            _context.Customer.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }
    }
}
