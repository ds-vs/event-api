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

        /// <summary> Создать учетную запись пользователя. </summary>
        /// <remarks> Пример запроса: POST api/account/register. </remarks> 
        /// <returns> Сообщение о создании учетной записи. </returns>
        [HttpPost]
        [Route("account/register")]
        public async Task<IActionResult> RegisterAsync(AccountDto request)
        {
            var response = await _accountService.RegisterAsync(request);

            if (response.Status == HttpStatusCode.OK)
                return Ok(response.Description);

            return BadRequest();
        }

        /// <summary> Сгенерировать токен для учетной записи пользователя. </summary>
        /// <remarks> Пример запроса: POST api/account/login. </remarks> 
        /// <returns> Токен. </returns>
        [HttpPost]
        [Route("account/login")]
        public async Task<IActionResult> LoginAsync(AccountDto request)
        {
            var response = await _accountService.LoginAsync(request);

            if (response.Status == HttpStatusCode.OK)
                return Ok(response.Data);

            return BadRequest(response.Description);
        }
    }
}
