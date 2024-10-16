using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Security.CVV
{
    public class CVVGenerator(IConfiguration configuration) : ICVVGenerator
    {
        public string GenerateCVV(string cardNumber, DateTime expirationDate)
        {
            string expirationDateString = expirationDate.ToString("MMyy");

            string data = cardNumber + expirationDateString;

            var key = configuration["JwtSettings:Secret"] ?? string.Empty;
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));

                string hashString = BitConverter.ToString(hash).Replace("-", "");

                string cvv = new string(hashString.Where(char.IsDigit).Take(3).ToArray());

                if (cvv.Length < 3)
                {
                    throw new InvalidOperationException("Failed to generate a valid CVV.");
                }

                return cvv;
            }
        }
    }
}
