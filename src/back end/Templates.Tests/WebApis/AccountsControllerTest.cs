using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Templates.Application.Authentications;
using Templates.Application.Users;
using Templates.EntityFrameworkCore.Entities;
using Templates.WebApi.Controllers;
using Templates.WebApi.Dtos.Accounts;
using Xunit;

namespace Templates.Tests.WebApis
{
    public class AccountsControllerTest : ApiControllerTest<AccountsController>
    {
        private readonly Mock<IAuthenticationAppService> _mockAuthAppService;

        public AccountsControllerTest()
        {
            _mockAuthAppService = new Mock<IAuthenticationAppService>();
            _controller = new AccountsController(_mockAuthAppService.Object);
        }

        [Fact]
        public async Task LoginAsync_ReturnsActionResult_WhenLoginDtoValid()
        {
            //var dto = GetTestLoginDto();

            //_mockUserAppService.Setup(m => m.LoginAsync(dto.UserNameOrEmail, dto.Password))
            //    .ReturnsAsync(new User()
            //    {
            //        Id = 1,
            //        UserName = dto.UserNameOrEmail,
            //        Email = dto.UserNameOrEmail
            //    });

            //var result = await _controller.LoginAsync(dto, );
        }


        private static LoginDto GetTestLoginDto()
        {
            return new LoginDto()
            {
                UserNameOrEmail = "test",
                Password = "123456"
            };
        }
    }
}
