using PortfolioInvestimentos.Domain.Commands.Contracts;

namespace PortfolioInvestimentos.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}
