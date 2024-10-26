using ProjectBank.BusinessLogic.Features.Credits.Commands;
using ProjectBank.BusinessLogic.Models;

namespace ProjectBank.BusinessLogic.Features.Credits.Service
{
    public interface ICreditLogicService
    {
        Task<CreditDto> CreateCredit(CreateCreditCommand creditCommand);
    }
}