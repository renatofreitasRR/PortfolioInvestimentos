using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Handlers;
using PortfolioInvestimentos.Domain.Api.Controllers.Contracts;
using System.Net;
using PortfolioInvestimentos.Domain.Repositories;
using PortfolioInvestimentos.Domain.Commands.Transactions;
using PortfolioInvestimentos.Domain.Entities;

namespace PortfolioInvestimentos.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        //[Authorize(Roles = "Manager")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _transactionRepository.GetAllAsync();

            return new CustomActionResult(HttpStatusCode.OK, users);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetExtractAsync(int userId)
        {
            var users = await _transactionRepository.GetExtractTransactions(userId);

            return new CustomActionResult(HttpStatusCode.OK, users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _transactionRepository
                .GetWithParamsAsync(x => x.Id == id);

            return new CustomActionResult(HttpStatusCode.OK, user);
        }

        [HttpPost]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> BuyTransactionsAsync([FromServices] TransactionHandler handler, [FromBody] BuyTransactionCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(HttpStatusCode.Created, commandResult.Data, commandResult.Errors);
        }

        [HttpPost]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> SellTransactionsAsync([FromServices] TransactionHandler handler, [FromBody] SellTransactionCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(HttpStatusCode.Created, commandResult.Data, commandResult.Errors);
        }

    }
}
