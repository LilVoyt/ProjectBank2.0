﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Services.Customers
{
    public class CustomerService(IDataContext context) : ICustomerService
    {
        public async Task<List<Customer>> Get(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<Customer> customers = context.Customer;

            if (!string.IsNullOrEmpty(search))
            {
                customers = customers.Where(n => n.FirstName.ToLower().Contains(search.ToLower()));
            }

            Expression<Func<Customer, object>> selectorKey = sortItem?.ToLower() switch
            {
                "name" => customer => customer.FirstName,
                "lastname" => customer => customer.LastName,
                "country" => customer => customer.Country,
                "phone" => customer => customer.PhoneNumber,
                "email" => customer => customer.Email,
                _ => customers => customers.FirstName
            };

            customers = sortOrder?.ToLower() == "desc"
                ? customers.OrderByDescending(selectorKey)
                : customers.OrderBy(selectorKey);

            List<Customer> customerList = await customers.ToListAsync();

            return customerList;
        }

        public async Task<Customer> Post(Customer customer)
        {
            await context.Customer.AddAsync(customer);
            await context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Guid id, Customer requestModel)
        {
            var account = await context.Customer.FindAsync(id);
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
            var account = await context.Customer.FindAsync(id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            context.Customer.Remove(account);
            await context.SaveChangesAsync();

            return account;
        }
    }
}
