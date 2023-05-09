using Event.API.Controllers;
using Event.Domain.Dto.Account;
using Event.Domain.Repositories;
using Event.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Event.Test.ControllerTests
{
    public class AccountControllerTests
    {
        private readonly IConfiguration _configuration;
        public AccountControllerTests()
        {
            _configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(@"appsettings.json", false, false)
               .AddEnvironmentVariables()
               .Build();
        }

        [Fact]
        public async Task RegisterAsync_ReturnsOk_WhenDataIsValid()
        {
            // Setup
            var mock = new Mock<IAccountRepository>();

            var accountDto = new RegisterAccountDto()
            {
                Login = "ds-vs",
                Email = "test@gmail.com",
                Password = "qwerty",
                RoleId = 1,
            };

            var service = new AccountService(mock.Object, _configuration);

            var controller = new AccountController(service);

            // Act
            var result = await controller.RegisterAsync(accountDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsBadRequestResult_WhenDataIsInvalid()
        {
            // Setup
            var mock = new Mock<IAccountRepository>();

            var service = new AccountService(mock.Object, _configuration);

            var controller = new AccountController(service);

            // Act
            var result = await controller.RegisterAsync(null!);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
