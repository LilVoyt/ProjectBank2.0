using AutoMapper;
using MediatR;
using ProjectBank.BusinessLogic.Features.Customers.Queries;
using ProjectBank.BusinessLogic.Features.Transactions.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Transactions.Handlers
{
    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, List<TransactionDto>>
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public GetTransactionQueryHandler(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public async Task<List<TransactionDto>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionService.Get(request.Sender, request.Receiver, request.SortItem, request.SortOrder);

            List<TransactionDto> transactionDtos = transactions.Select(transaction => _mapper.Map<TransactionDto>(transaction)).ToList();

            return transactionDtos;
        }


    }
}
