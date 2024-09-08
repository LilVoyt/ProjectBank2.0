﻿using Microsoft.AspNetCore.Mvc;
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
        Task<ActionResult<List<Card>>> Get(string? search, string? sortItem, string? sortOrder);
        Task<Card> Post(Card card);
        Task<Card> Update(Guid id, Card requestModel);
        Task<Card> Delete(Guid id);
    }
}
