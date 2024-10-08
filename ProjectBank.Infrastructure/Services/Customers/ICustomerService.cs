﻿using Microsoft.AspNetCore.Mvc;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Services.Customers
{
    public interface ICustomerService
    {
        Task<List<Customer>> Get(string? search, string? sortItem, string? sortOrder);
        Task<Customer> Post(Customer customer);
        Task<Customer> Update(Guid id, Customer requestModel);
        Task<Customer> Delete(Guid id);
    }
}
