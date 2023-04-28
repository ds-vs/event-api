using Event.Domain.Dto.Account;
using Event.Domain.Entities;
using Event.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Event.API.Controllers
{
    /// <summary> Контроллер, работающий с AccountService. </summary>
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("account/register")]
        public async Task<IActionResult> RegisterAsync(AccountDto account)
        {
            var response = await _accountService.RegisterAsync(account);

            if (response.Status == HttpStatusCode.OK)
                return Ok("Account created successfully.");

            return BadRequest();
        }
    }
}
