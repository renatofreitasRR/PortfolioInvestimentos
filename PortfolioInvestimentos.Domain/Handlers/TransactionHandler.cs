using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Commands.Transactions;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Enums;
using PortfolioInvestimentos.Domain.Handlers.Contracts;
using PortfolioInvestimentos.Domain.Repositories;
using System.Net;

namespace PortfolioInvestimentos.Domain.Handlers
{
    public class TransactionHandler
        : IHandler<BuyTransactionCommand>,
          IHandler<SellTransactionCommand>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;

        public TransactionHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IProductRepository productRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _productRepository = productRepository;
        }

        public async Task<ICommandResult> Handle(BuyTransactionCommand command)
        {
            var product = await _productRepository.GetWithParamsAsync(x => x.Id == command.ProductId);

            if (product is null)
                return new CommandResult(HttpStatusCode.NotFound, null, $"Produto não encontrado!");

            var account = await _accountRepository.GetWithParamsAsync(x => x.Id == command.AccountId);

            if (account is null)
                return new CommandResult(HttpStatusCode.NotFound, null, $"Conta não encontrada!");

            if (command.Quantity > product.QuantityAvailable)
                return new CommandResult(HttpStatusCode.BadRequest, null, $"Não é possível comprar mais cotas  do que o disponível. Atualmente há {product.QuantityAvailable} cotas disponíveis para o produto {product.Name}!");

            var transactionTotalValue = product.CalculateTotalTransactionValue(command.Quantity);

            var transaction = new Transaction(
              command.AccountId,
              command.ProductId,
              command.Quantity,
              OperationType.Buy,
              transactionTotalValue
              );

            var accountValidation = account.Withdraw(transactionTotalValue);

            if (accountValidation.isValid is false)
                return new CommandResult(HttpStatusCode.BadRequest, null, accountValidation.message);

            var productValidation = product.UpdateQuantityAvailable(command.Quantity, OperationType.Buy);

            if (productValidation.isValid is false)
                return new CommandResult(HttpStatusCode.BadRequest, null, productValidation.message);

            await _transactionRepository.CreateAsync(transaction);

            _accountRepository.Update(account);
            _productRepository.Update(product);

            await _transactionRepository.SaveAsync();

            return new CommandResult();
        }

        public async Task<ICommandResult> Handle(SellTransactionCommand command)
        {
            var product = await _productRepository.GetWithParamsAsync(x => x.Id == command.ProductId);

            if (product is null)
                return new CommandResult(HttpStatusCode.NotFound, null, $"Produto não encontrado!");

            var account = await _accountRepository.GetWithParamsAsync(x => x.Id == command.AccountId);

            if (account is null)
                return new CommandResult(HttpStatusCode.NotFound,null, $"Conta não encontrada!");

            var purchasedTransactions = await _transactionRepository
            .GetAllWithParamsAsync(
            x =>
            x.ProductId == command.ProductId
            &&
            x.AccountId == command.AccountId
            &&
            x.OperationType == OperationType.Buy
            );

            var totalPurchasedTransactions = purchasedTransactions
                .Sum(x => x.Quantity);

            if (command.Quantity > totalPurchasedTransactions)
                return new CommandResult(
                    HttpStatusCode.BadRequest,
                    null,
                    $"Não é possível vender mais cotas do que você possui. Atualmente você tem {totalPurchasedTransactions} cotas disponíveis para o produto {product.Name}!"
                    );

            var transactionTotalValue = product.CalculateTotalTransactionValue(command.Quantity);

            var transaction = new Transaction(
                command.AccountId,
                command.ProductId,
                command.Quantity,
                OperationType.Sell,
                transactionTotalValue
                );

            var accountValidation = account.Deposit(transactionTotalValue);

            if (accountValidation.isValid is false)
                return new CommandResult(HttpStatusCode.BadRequest, null, accountValidation.message);

            var productValidation = product.UpdateQuantityAvailable(command.Quantity, OperationType.Sell);

            if (productValidation.isValid is false)
                return new CommandResult(HttpStatusCode.BadRequest, null, productValidation.message);

            await _transactionRepository.CreateAsync(transaction);

            _accountRepository.Update(account);
            _productRepository.Update(product);

            await _transactionRepository.SaveAsync();

            return new CommandResult();
        }
    }
}
