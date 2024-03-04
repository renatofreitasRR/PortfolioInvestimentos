using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Commands.Users;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Handlers.Contracts;
using PortfolioInvestimentos.Domain.Services;
using PortfolioInvestimentos.Domain.Repositories;

namespace PortfolioInvestimentos.Domain.Handlers
{
    public class UserHandler : IHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPasswordService _userPasswordService;

        public UserHandler(IUserRepository userRepository, IUserPasswordService userPasswordService)
        {
            _userRepository = userRepository;
            _userPasswordService = userPasswordService;
        }

        public async Task<ICommandResult> Handle(CreateUserCommand command)
        {
            var userExists = await _userRepository
                .ExistsAsync(x => x.Email.ToUpper() == command.Email.ToUpper());

            if (userExists)
                return new CommandResult(null, $"O usuário com email {command.Email} já existe!");

            var user = new User(command.Name, command.Profile, command.Email, command.Role);

            var passwordHash = _userPasswordService.HashPassword(user, command.Password);

            user.SetPassword(passwordHash);

            await _userRepository.CreateAsync(user);
            await _userRepository.SaveAsync();

            return new CommandResult();
        }
    }
}
