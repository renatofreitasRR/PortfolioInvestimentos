using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Handlers;
using PortfolioInvestimentos.Domain.Api.Controllers.Contracts;
using System.Net;
using PortfolioInvestimentos.Domain.Repositories;
using PortfolioInvestimentos.Domain.Commands.Transactions;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using PortfolioInvestimentos.Domain.Api.Models;
using System.Text.Json;

namespace PortfolioInvestimentos.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IDistributedCache _cache;

        public TransactionController(ITransactionRepository transactionRepository, IDistributedCache cache)
        {
            _transactionRepository = transactionRepository;
            _cache = cache;
        }

        [HttpGet]
        //[Authorize(Roles = "Manager")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();

            return new CustomActionResult(HttpStatusCode.OK, transactions);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetExtractAsync(int userId)
        {
            var transactions = await _transactionRepository.GetExtractTransactions(userId);

            return new CustomActionResult(HttpStatusCode.OK, transactions);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetExtractPaginatedAsync([FromQuery] PaginationParams paginationParams, int userId)
        {
            var transactionsCache = await _cache.GetStringAsync(
                $"transactions-{userId}");

            if (transactionsCache != null)
            {
                var transactionsJson = JsonSerializer.Deserialize<IEnumerable<Transaction>>(transactionsCache);

                return new CustomActionResult(HttpStatusCode.OK, transactionsJson);
            }

            //var transactions = await _transactionRepository
            //    .GetExtractTransactions(paginationParams, userId);

            var transactions = await _transactionRepository
                .GetAllAsync();

            var json = JsonSerializer.Serialize<IEnumerable<Transaction>>(transactions);

            await _cache.SetStringAsync(
                $"transactions-{userId}",
                json,
                CacheOptions.DefaultExpiration);

            return new CustomActionResult(HttpStatusCode.OK, transactions);
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
