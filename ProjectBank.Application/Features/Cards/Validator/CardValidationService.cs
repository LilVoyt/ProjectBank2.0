using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Cards.Cards
{
    public class CardValidationService : ICardValidationService
    {
        private readonly DataContext _context;

        public CardValidationService(DataContext dataContext)
        {
            _context = dataContext;
        }
    }
}
