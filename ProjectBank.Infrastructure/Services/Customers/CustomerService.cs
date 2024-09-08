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

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> Get(string? search, string? sortItem, string? sortOrder)
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

            return customerList;
        }

        public async Task<Customer> Post(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Guid id, Customer requestModel)
        {
            var account = await _context.Customer.FindAsync(id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            //account = _customerMapper.PutRequestModelInCustomer(account, requestModel);
            //var validationResult = await _validator.ValidateAsync(account);
            //if (!validationResult.IsValid)
            //{
            //    var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            //    throw new ValidationException(errorMessages);
            //}
            //_context.Customer.Update(account);
            //await _context.SaveChangesAsync();

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
