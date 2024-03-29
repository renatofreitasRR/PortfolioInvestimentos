﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioInvestimentos.Domain.Api.Controllers.Contracts;
using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Handlers;
using PortfolioInvestimentos.Domain.Repositories;
using System.Net;
using PortfolioInvestimentos.Domain.Commands.Accounts;

namespace PortfolioInvestimentos.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();

            return new CustomActionResult(HttpStatusCode.OK, accounts);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var account = await _accountRepository
                .GetWithParamsAsync(x => x.Id == id);
            
            if(account == null)
                return new CustomActionResult(HttpStatusCode.NotFound, $"A conta com id {id} não foi encontrada", isData: false);

            return new CustomActionResult(HttpStatusCode.OK, account);
        }

        [HttpPost]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> PostAsync([FromServices] AccountHandler handler, [FromBody] CreateAccountCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
        }

        [HttpPut]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> DepositAsync([FromServices] AccountHandler handler, [FromBody] DepositAccountCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
        }

        [HttpPut]
        [Authorize(Roles = "Client, Manager")]
        public async Task<IActionResult> WithdrawAsync([FromServices] AccountHandler handler, [FromBody] WithdrawAccountCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
        }
    }
}
