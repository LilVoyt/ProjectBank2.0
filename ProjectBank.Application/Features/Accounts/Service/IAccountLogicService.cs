using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Models;

namespace ProjectBank.BusinessLogic.Features.Accounts.Service
{
    public interface IAccountLogicService
    {
        Task<AccountDto> GetDto(GetByIdQuery request);
    }
}