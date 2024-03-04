using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Commands.Products;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Handlers.Contracts;
using PortfolioInvestimentos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return new CommandResult(null, $"O Produto com nome {command.Name} já existe!");

            var product = new Product(command.Name, command.Type, command.Value, command.DueDate);

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveAsync();

            return new CommandResult();
        }

        public async Task<ICommandResult> Handle(UpdateProductCommand command)
        {
            var productExists = await _productRepository
                .ExistsAsync(x => x.Id == command.Id);

            if (productExists is false)
                return new CommandResult(null, $"Produto não encontrado!");

            var product = await _productRepository.GetWithParamsAsync(x => x.Id == command.Id);

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return new CommandResult();
        }
    }
}
