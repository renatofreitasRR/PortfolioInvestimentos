using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]")]
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
            var users = await _accountRepository.GetAllAsync();

            return new CustomActionResult(HttpStatusCode.OK, users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _accountRepository
                .GetWithParamsAsync(x => x.Id == id);

            return new CustomActionResult(HttpStatusCode.OK, user);
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> PostAsync([FromServices] AccountHandler handler, [FromBody] CreateAccountCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
        }
    }
}
