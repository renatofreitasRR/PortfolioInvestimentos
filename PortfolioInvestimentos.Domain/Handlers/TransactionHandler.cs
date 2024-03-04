using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Commands.Transactions;
using PortfolioInvestimentos.Domain.Handlers.Contracts;
using PortfolioInvestimentos.Domain.Repositories;

namespace PortfolioInvestimentos.Domain.Handlers
{
    public class TransactionHandler : IHandler<CreateTransactionCommand>
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ICommandResult> Handle(CreateTransactionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
