using Microsoft.Identity.Client;
using ProjectBank.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.CardManagement
{
    public interface ICardManagementService
    {
        Task<Guid> CreateCardAsync(string Pincode, string CardName, string CurrencyCode, Guid AccountID);
        Task<List<CardDto>> GetCardInfo(Guid AccountId);
    }
}
