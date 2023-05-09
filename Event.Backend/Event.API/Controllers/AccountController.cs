using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Event.API.Controllers
{
    /// <summary> Контроллер, работающий с AccountService. </summary>
    [ApiController, Route("api/")]
    [ApiExplorerSettings(GroupName = "Account")]
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
        [HttpPost, Route("account/register")]
        public async Task<IActionResult> RegisterAsync(RegisterAccountDto request)
        {
            var response = await _accountService.RegisterAsync(request);

            if (response.Status == HttpStatusCode.OK)
                return Ok(response.Description);

            return BadRequest();
        }

        /// <summary> Сгенерировать токен для учетной записи пользователя. </summary>
        /// <remarks> Пример запроса: POST api/account/login. </remarks> 
        /// <returns> Токен. </returns>
        [HttpPost, Route("account/login")]
        public async Task<IActionResult> LoginAsync(LoginAccountDto request)
        {
            var response = await _accountService.LoginAsync(request);

            if (response.Status == HttpStatusCode.OK)
            {
                var cookieOptions = new CookieOptions { HttpOnly = true, Expires = response.Data!.TokenExpires };
                Response.Cookies.Append("refreshToken", response.Data.RefreshToken, cookieOptions);

                return Ok(response.Data.Token);
            }

            return BadRequest(response.Description);
        }

        /// <summary> Обновить токен для учетной записи пользователя. </summary>
        /// <remarks> Пример запроса: POST api/account/refreshtoken. </remarks> 
        /// <returns> Токен. </returns>
        [HttpPost, Authorize, Route("account/refreshtoken")]
        public async Task<ActionResult<string>> RefreshTokenAsync()
        {
            var login = User?.Identity?.Name;

            var response = await _accountService.GetRefreshTokenAsync(login);
            var refreshToken = Request.Cookies["refreshToken"];

            if (response.Status == HttpStatusCode.OK)
            {
                if (!response.Data!.RefreshToken.Equals(refreshToken))
                    return Unauthorized("Invalid Refresh Token.");
                else if (response.Data.TokenExpires < DateTime.Now)
                    return Unauthorized("Token expired.");

                response = await _accountService.NewRefreshTokenAsync(login);

                if (response.Status == HttpStatusCode.OK)
                {
                    var cookieOptions = new CookieOptions { HttpOnly = true, Expires = response.Data!.TokenExpires };
                    Response.Cookies.Append("refreshToken", response.Data.RefreshToken, cookieOptions);

                    return Ok(response.Data!.Token);
                }
            }
            return BadRequest(response.Description);
        }
    }
}
