using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Commands.Accounts;
using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Handlers.Contracts;
using PortfolioInvestimentos.Domain.Repositories;
using System.Net;

namespace PortfolioInvestimentos.Domain.Handlers
{
    public class AccountHandler : IHandler<CreateAccountCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountHandler(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        public async Task<ICommandResult> Handle(CreateAccountCommand command)
        {
            var userExists = await _userRepository
                .ExistsAsync(x => x.Id == command.UserId);

            if (userExists is false)
                return new CommandResult(HttpStatusCode.NotFound, null, $"O Usuário com id {command.UserId} não existe!");

            var accountExists = await _accountRepository
                .ExistsAsync(x => x.UserId == command.UserId);

            if (accountExists)
                return new CommandResult(HttpStatusCode.Conflict, null, $"O Usuário com id {command.UserId} já possui uma conta!");

            var account = new Account(command.UserId, command.Value);

            await _accountRepository.CreateAsync(account);
            await _accountRepository.SaveAsync();

            return new CommandResult();
        }
    }
}
