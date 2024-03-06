using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioInvestimentos.Domain.Commands;
using PortfolioInvestimentos.Domain.Commands.Users;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Handlers;
using PortfolioInvestimentos.Domain.Services;
using PortfolioInvestimentos.Domain.Api.Controllers.Contracts;
using PortfolioInvestimentos.Domain.Repositories;
using System.Net;
using AutoMapper;
using PortfolioInvestimentos.Domain.Models.Users;

namespace PortfolioInvestimentos.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserPasswordService _userPasswordService;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _tokenService;
        private readonly IMapper _mapper;

        public UserController(IUserPasswordService userPasswordService, IUserRepository userRepository, IJwtTokenService tokenService, IMapper mapper)
        {
            _userPasswordService = userPasswordService;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var usersMapping = _mapper.Map<IEnumerable<UserResult>>(users);

            return new CustomActionResult(HttpStatusCode.OK, usersMapping);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager, Client")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userRepository
                .GetWithParamsAsync(x => x.Id == id);

            if (user == null)
                return new CustomActionResult(HttpStatusCode.NotFound, $"O usuário com {id} não foi encontrado", isData: false);

            var userMapping = _mapper.Map<UserResult>(user);

            return new CustomActionResult(HttpStatusCode.OK, userMapping);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromServices] UserHandler handler, [FromBody] CreateUserCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
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
