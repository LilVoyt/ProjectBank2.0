using Microsoft.EntityFrameworkCore;
using ProjectBank.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Validators.Accounts
{
    internal class AccountValidationService : IAccountValidationService
    {
        private readonly DataContext _context;

        public AccountValidationService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> IsCustomerExists(Guid customerId, CancellationToken cancellationToken)
        {
            return await _context.Customer.AnyAsync(e => e.Id == customerId);
        }

        public async Task<bool> IsEmployeeExistsOrNull(Guid? employeeId, CancellationToken cancellationToken)
        {
            if (employeeId == null)
            {
                return true;
            }
            return await _context.Employee.AnyAsync(e => e.Id == employeeId);
        }

        public async Task<bool> IsNotAlreadyRegisteredCustomer(Guid customerId, CancellationToken cancellationToken)
        {
            return !await _context.Account.AnyAsync(a => a.CustomerID == customerId);
        }

        public async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken)
        {
            return !await _context.Account.AnyAsync(a => a.Name == name);
        }
    }
}