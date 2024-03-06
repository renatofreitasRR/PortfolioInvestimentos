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
using PortfolioInvestimentos.Application.Caching.Contracts;

namespace PortfolioInvestimentos.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICachingService _cachingService;

        public TransactionController(ITransactionRepository transactionRepository, ICachingService cachingService)
        {
            _transactionRepository = transactionRepository;
            _cachingService = cachingService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();

            return new CustomActionResult(HttpStatusCode.OK, transactions);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetExtractPaginatedAsync([FromQuery] PaginationParams paginationParams, int userId)
        {
            var transactionsCached = await _cachingService.GetAsync<PagedList<InvestmentStatement?>>($"transactions-{paginationParams.PageSize}-{paginationParams.PageNumber}");

            if (transactionsCached != null)
                return new CustomActionResult(HttpStatusCode.OK, transactionsCached);

            var transactions = _transactionRepository
                .GetExtractTransactions(paginationParams, userId);

            await _cachingService.SetAsync<PagedList<InvestmentStatement?>>($"transactions-{paginationParams.PageSize}-{paginationParams.PageNumber}", transactions);

            return new CustomActionResult(HttpStatusCode.OK, transactions);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _transactionRepository
                .GetWithParamsAsync(x => x.Id == id);

            if (user == null)
                return new CustomActionResult(HttpStatusCode.NotFound, $"A transação com {id} não foi encontrada", isData: false);

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
