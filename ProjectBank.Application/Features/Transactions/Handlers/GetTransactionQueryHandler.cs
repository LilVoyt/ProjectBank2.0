using AutoMapper;
using MediatR;
using ProjectBank.BusinessLogic.Features.Transactions.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Services.Transactions;

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
