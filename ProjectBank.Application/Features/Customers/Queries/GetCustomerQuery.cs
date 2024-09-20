using MediatR;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Customers.Queries
{
    public class GetCustomerQuery : IRequest<List<Customer>>
    {
        public string? Search { get; set; }
        public string? SortItem { get; set; }
        public string? SortOrder { get; set; }
    }
}