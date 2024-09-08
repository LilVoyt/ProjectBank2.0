using Microsoft.EntityFrameworkCore;
using ProjectBank.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Validators.Cards
{
    internal class CardValidationService : ICardValidationService
    {
        private readonly DataContext _context;

        public CardValidationService(DataContext dataContext)
        {
            _context = dataContext;
        }
    }
}
