using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Validators.Customers
{
    public interface ICustomerValidationService
    {
        Task<bool> Is_PhoneNumber_Valid(string number, CancellationToken cancellationToken);
        Task<bool> Is_PhoneNumber_Not_In_DB(string number, CancellationToken cancellationToken);
        Task<bool> Is_Email_Not_In_DB(string email, CancellationToken cancellationToken);
    }
}
