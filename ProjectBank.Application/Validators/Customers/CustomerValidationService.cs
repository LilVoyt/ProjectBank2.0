using Microsoft.EntityFrameworkCore;
using ProjectBank.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectBank.Application.Validators.Customers
{
    public class CustomerValidationService : ICustomerValidationService
    {
        private readonly DataContext _context;

        public CustomerValidationService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Is_Email_Not_In_DB(string email, CancellationToken cancellationToken)
        {
            return !await _context.Customer.AnyAsync(c => c.Email == email);
        }

        public async Task<bool> Is_PhoneNumber_Not_In_DB(string number, CancellationToken cancellationToken)
        {
            return !await _context.Customer.AnyAsync(c => c.PhoneNumber == number);
        }

        public Task<bool> Is_PhoneNumber_Valid(string phoneNumber, CancellationToken cancellationToken)
        {
            string phonePattern = @"^\+?3?8?(0\d{9}|8\d{9})$";
            bool isValid = Regex.IsMatch(phoneNumber, phonePattern);
            return Task.FromResult(isValid);
        }

    }
}