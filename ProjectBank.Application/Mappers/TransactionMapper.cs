using ProjectBank.Application.Models;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Mappers
{
    internal class TransactionMapper
    {
        public Transaction GetTransaction(TransactionRequestModel requestModel)
        {
            var transaction = new Transaction();
            transaction.Id = Guid.NewGuid();
            transaction.TransactionDate = requestModel.TransactionDate;
            transaction.Sum = requestModel.Sum;
            transaction.CardSenderID = requestModel.CardSenderID;
            transaction.CardReceiverID = requestModel.CardReceiverID;

            return transaction;
        }

        public TransactionRequestModel GetRequestModel(Transaction transaction)
        {
            var requestModel = new TransactionRequestModel();
            requestModel.TransactionDate = transaction.TransactionDate;
            requestModel.Sum = transaction.Sum;
            requestModel.CardSenderID = transaction.CardSenderID;
            requestModel.CardReceiverID = transaction.CardReceiverID;
            return requestModel;
        }


        public Transaction PutRequestModelInTransaction(Transaction res, TransactionRequestModel transaction)
        {
            res.TransactionDate = transaction.TransactionDate;
            res.Sum = transaction.Sum;
            res.CardSenderID = transaction.CardSenderID;
            res.CardReceiverID = transaction.CardReceiverID;

            return res;
        }
        public List<TransactionRequestModel> GetRequestModels(List<Transaction> transactions)
        {
            return transactions.Select(transaction => GetRequestModel(transaction)).ToList();
        }
    }
}
