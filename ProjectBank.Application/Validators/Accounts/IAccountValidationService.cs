using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Validators.Accounts
{
    public interface IAccountValidationService
    {
        Task<bool> IsCustomerExists(Guid customerID, CancellationToken cancellationToken);
        Task<bool> IsEmployeeExistsOrNull(Guid? employeeID, CancellationToken cancellationToken);
        Task<bool> IsNameUnique(string name, CancellationToken cancellationToken);
        Task<bool> IsNotAlreadyRegisteredCustomer(Guid customerID, CancellationToken cancellationToken);
    }
}
