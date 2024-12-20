﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid CardSenderID { get; set; } = Guid.Empty;
        public Guid CardReceiverID { get; set; } = Guid.Empty;
        public virtual Card CardSender { get; set; }
        public virtual Card CardReceiver { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
