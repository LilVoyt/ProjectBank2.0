using Microsoft.AspNetCore.Mvc;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Infrastructure.Services.Customers
{
    public interface ICustomerService
    {
        Task<ActionResult<List<CustomerRequestModel>>> Get(string? search, string? sortItem, string? sortOrder);
        Task<Customer> Post(CustomerRequestModel customer);
        Task<Customer> Update(Guid id, CustomerRequestModel requestModel);
        Task<Customer> Delete(Guid id);
    }
}
