using ProjectBank.Application.Models;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Mappers
{
    internal class CustomerMapper
    {
        public Customer GetCustomer(CustomerRequestModel requestModel)
        {
            var customer = new Customer();
            customer.Id = Guid.NewGuid();
            customer.Name = requestModel.Name;
            customer.LastName = requestModel.LastName;
            customer.Country = requestModel.Country;
            customer.Phone = requestModel.Phone;
            customer.Email = requestModel.Email;

            return customer;
        }


        public CustomerRequestModel GetRequestModel(Customer customer)
        {
            var requestModel = new CustomerRequestModel();
            requestModel.Name = customer.Name;
            requestModel.LastName = customer.LastName;
            requestModel.Country = customer.Country;
            requestModel.Phone = customer.Phone;
            requestModel.Email = customer.Email;

            return requestModel;
        }

        public Customer PutRequestModelInCustomer(Customer res, CustomerRequestModel customer)
        {
            res.Name = customer.Name;
            res.LastName = customer.LastName;
            res.Country = customer.Country;
            res.Phone = customer.Phone;
            res.Email = customer.Email;

            return res;
        }
        public List<CustomerRequestModel> GetRequestModels(List<Customer> customers)
        {
            return customers.Select(customer => GetRequestModel(customer)).ToList();
        }
    }
}
