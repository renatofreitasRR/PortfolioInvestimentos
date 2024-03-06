using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Handlers;
using PortfolioInvestimentos.Domain.Api.Controllers.Contracts;
using System.Net;
using PortfolioInvestimentos.Domain.Repositories;
using PortfolioInvestimentos.Domain.Commands.Products;
using PortfolioInvestimentos.Domain.Models;
using PortfolioInvestimentos.Application.Caching.Contracts;
using PortfolioInvestimentos.Domain.Entities;

namespace PortfolioInvestimentos.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICachingService _cachingService;

        public ProductController(IProductRepository productRepository, ICachingService cachingService)
        {
            _productRepository = productRepository;
            _cachingService = cachingService;
        }

        [HttpGet]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetAllPagedAsync([FromQuery] PaginationParams paginationParams)
        {
            var productsCached = await _cachingService.GetAsync<PagedList<Product>>($"products-{paginationParams.PageSize}-{paginationParams.PageNumber}");

            if (productsCached != null)
                return new CustomActionResult(HttpStatusCode.OK, productsCached);

            var products = _productRepository
                .GetProductsPaged(paginationParams);

            await _cachingService.SetAsync<PagedList<Product?>>($"products-{paginationParams.PageSize}-{paginationParams.PageNumber}", products);

            return new CustomActionResult(HttpStatusCode.OK, products);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productRepository
                .GetWithParamsAsync(x => x.Id == id);

            if (product == null)
                return new CustomActionResult(HttpStatusCode.NotFound, $"O Produto com {id} não foi encontrado", isData: false);

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
