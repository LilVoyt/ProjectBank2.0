﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Entities
{
    public class Card
    {
        public Guid Id { get; set; }
        public string NumberCard { get; set; }
        public string CardName { get; set; }
        public string Pincode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CVV { get; set; }
        public decimal Balance { get; set; }
        public Guid CurrencyID { get; set; }
        public Guid AccountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<Transaction> SentTransactions { get; set; }
        public virtual ICollection<Transaction> ReceivedTransactions { get; set; }
        public virtual ICollection<Credit> Credits { get; set; } 
    }
}
