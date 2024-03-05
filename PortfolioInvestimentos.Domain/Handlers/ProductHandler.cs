using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Commands.Products;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Handlers.Contracts;
using PortfolioInvestimentos.Domain.Repositories;
using System.Net;

namespace PortfolioInvestimentos.Domain.Handlers
{
    public class ProductHandler : IHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ICommandResult> Handle(CreateProductCommand command)
        {
            var productExists = await _productRepository
                .ExistsAsync(x => x.Name == command.Name);

            if (productExists)
                return new CommandResult(HttpStatusCode.Conflict, null, $"O Produto com nome {command.Name} já existe!");

            var product = new Product(command.Name, command.Type, command.Value, command.QuantityAvailable, command.DueDate);

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveAsync();

            return new CommandResult();
        }

        public async Task<ICommandResult> Handle(UpdateProductCommand command)
        {
            var product = await _productRepository.GetWithParamsAsync(x => x.Id == command.Id);

            if (product is null)
                return new CommandResult(HttpStatusCode.NotFound, null, $"Produto não encontrado!");

            var existsWithSameName = await _productRepository
                .ExistsAsync(x => x.Name.ToUpper() == command.Name.ToUpper() && x.Id != command.Id);

            if (existsWithSameName)
                return new CommandResult(HttpStatusCode.Conflict, null, $"O Produto {command.Name} já existe!");

            product.Update(command.Name, command.Type, command.Value, command.QuantityAvailable, command.DueDate, command.IsActive);

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return new CommandResult();
        }
    }
}
