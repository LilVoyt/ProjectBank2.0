using AutoMapper;
using MediatR;
using ProjectBank.BusinessLogic.Features.Transactions.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Services.Transactions;

namespace ProjectBank.BusinessLogic.Features.Transactions.Handlers
{
    public class GetTransactionQueryHandler(ITransactionService transactionService, IMapper mapper) : IRequestHandler<GetTransactionQuery, List<TransactionDto>>
    {
        public async Task<List<TransactionDto>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var transactions = await transactionService.Get(request.Sender, request.Receiver, request.SortItem, request.SortOrder);

            List<TransactionDto> transactionDtos = transactions.Select(transaction => mapper.Map<TransactionDto>(transaction)).ToList();

            return transactionDtos;
        }


    }
}
