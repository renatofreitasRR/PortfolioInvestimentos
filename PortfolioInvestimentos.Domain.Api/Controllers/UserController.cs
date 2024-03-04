using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Commands.Contracts;
using PortfolioInvestimentos.Domain.Commands.Users;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Enums;
using PortfolioInvestimentos.Domain.Handlers;
using PortfolioInvestimentos.Domain.Services;
using PortfolioInvestimentos.Domain.Api.Controllers.Contracts;
using PortfolioInvestimentos.Domain.Repositories;
using System.Net;

namespace PortfolioInvestimentos.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserPasswordService _userPasswordService;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _tokenService;

        public UserController(IUserPasswordService userPasswordService, IUserRepository userRepository, IJwtTokenService tokenService)
        {
            _userPasswordService = userPasswordService;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpGet]
        //[Authorize(Roles = "1")]
        //Manager
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return new CustomActionResult(HttpStatusCode.OK, users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userRepository
                .GetWithParamsAsync(x => x.Id == id);

            return new CustomActionResult(HttpStatusCode.OK, user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromServices] UserHandler handler, [FromBody] CreateUserCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(HttpStatusCode.Created, commandResult.Data, commandResult.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var user = await _userRepository.GetWithParamsAsync(x => x.Id == id);
            _userRepository.Delete(user);
            await _userRepository.SaveAsync();

            return new CustomActionResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync([FromBody] UserLoginCommand command)
        {
            User user = await _userRepository
                .GetWithParamsAsync(x => x.Email.ToUpper() == command.Email.ToUpper());

            if (user == null)
                return new CustomActionResult(HttpStatusCode.NotFound, "Usuário não encontrado");

            bool result = _userPasswordService.VerifyPassword(user, user.PasswordHash, command.Password);

            if (result is false)
                return new CustomActionResult(HttpStatusCode.BadRequest, "Senha inválida");

            var token = _tokenService.GetToken(user);

            return new CustomActionResult(HttpStatusCode.OK, new { Token = token });
        }
    }
}
