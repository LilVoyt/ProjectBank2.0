using Microsoft.AspNetCore.Mvc;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Infrastructure.Services.Cards
{
    public interface ICardService
    {
        Task<ActionResult<List<CardRequestModel>>> Get(string? search, string? sortItem, string? sortOrder);
        Task<Card> Post(CardRequestModel card);
        Task<Card> Update(Guid id, CardRequestModel requestModel);
        Task<Card> Delete(Guid id);
    }
}
