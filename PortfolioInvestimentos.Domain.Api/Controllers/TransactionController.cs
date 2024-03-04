using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioInvestimentos.Domain.Commands.Products;
using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Handlers;
using PortfolioInvestimentos.Domain.Api.Controllers.Contracts;
using System.Net;
using PortfolioInvestimentos.Domain.Repositories;

namespace PortfolioInvestimentos.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _transactionRepository.GetAllAsync();

            return new CustomActionResult(HttpStatusCode.OK, users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _transactionRepository
                .GetWithParamsAsync(x => x.Id == id);

            return new CustomActionResult(HttpStatusCode.OK, user);
        }

        [HttpPost]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> PostAsync([FromServices] ProductHandler handler, [FromBody] CreateProductCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(HttpStatusCode.Created, commandResult.Data, commandResult.Errors);
        }

        [HttpPost]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> PutAsync([FromServices] ProductHandler handler, [FromBody] UpdateProductCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(HttpStatusCode.Created, commandResult.Data, commandResult.Errors);
        }
    }
}
