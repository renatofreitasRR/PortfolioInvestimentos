using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Handlers;
using PortfolioInvestimentos.Domain.Api.Controllers.Contracts;
using System.Net;
using PortfolioInvestimentos.Domain.Repositories;
using PortfolioInvestimentos.Domain.Commands.Products;

namespace PortfolioInvestimentos.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        //[Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return new CustomActionResult(HttpStatusCode.OK, products);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productRepository
                .GetWithParamsAsync(x => x.Id == id);

            return new CustomActionResult(HttpStatusCode.OK, product);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> PostAsync([FromServices] ProductHandler handler, [FromBody] CreateProductCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> PutAsync([FromServices] ProductHandler handler, [FromBody] UpdateProductCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
        }
    }
}
