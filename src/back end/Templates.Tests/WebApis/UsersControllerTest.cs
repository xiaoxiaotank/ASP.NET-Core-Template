using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Templates.Application.Users;
using Templates.EntityFrameworkCore.Entities;
using Templates.WebApi.Controllers;
using Templates.WebApi.Dtos.Users;
using Xunit;

namespace Templates.Tests.WebApis
{
    public class UsersControllerTest : ApiControllerTest<UsersController>
    {
        private readonly Mock<IUserAppService> _mockUserAppService;

        public UsersControllerTest()
        {
            _mockUserAppService = new Mock<IUserAppService>();
            _controller = new UsersController(_mockUserAppService.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetAll_ReturnsNotNullAndEmpty()
        {
            var testUsers = GetTestUsers();
            _mockUserAppService.Setup(m => m.Get()).Returns(testUsers);

            var result = _controller.GetAll();

            Assert.NotEmpty(result);
            Assert.Equal(result.Count(), testUsers.Count());
        }

        [Fact]
        public void GetByQuery_ReturnNotNullAndEmtpy_WhenExistQueryData()
        {
            var query = GetTestQueryDto();
            var testUsers = GetTestUsers();
            _mockUserAppService.Setup(m => m.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(GetTestUsers().Where(u => u.UserName.Contains(query.UserName) && u.CreationTime >= query.StartCreationTime));

            var result = _controller.GetByQuery(query);

            Assert.NotEmpty(result);
            Assert.Single(result);
            Assert.Equal(result.First().Id, testUsers.First().Id);
        }

        [Theory]
        [InlineData(1), InlineData(2)]
        public async Task GetByIdAsync_ReturnsUserDto_WhenIdValid(int id)
        {
            _mockUserAppService.Setup(m => m.GetAsync(id)).ReturnsAsync(GetTestUsers().SingleOrDefault(u => u.Id == id));

            var result = await _controller.GetByIdAsync(id);
            var value = result.Value;

            Assert.Equal(value.Id, id);
        }

        [Theory]
        [InlineData(-1), InlineData(999)]
        public async Task GetByIdAsync_ReturnsNotFound_WhenIdInvalid(int id)
        {
            var result = await _controller.GetByIdAsync(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        private static IQueryable<User> GetTestUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    UserName = "jjj",
                    CreationTime = DateTime.Parse("2019-1-14"),
                },
                new User()
                {
                    Id = 2,
                    UserName = "kkk",
                    CreationTime = DateTime.Parse("2018-1-14"),
                }
            }.AsQueryable();
        }

        private static UserQueryDto GetTestQueryDto()
        {
            return new UserQueryDto()
            {
                UserName = "j",
                StartCreationTime = DateTime.Parse("2019-1-1")
            };
        }
    }
}
