using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Templates.WebApi.Core.Controllers;

namespace Templates.Tests.WebApis
{
    public abstract class ApiControllerTest<TApiController> where TApiController : ApiController
    {
        protected TApiController _controller;
        protected readonly Mock<ILogger<TApiController>> _mockLogger;

        public ApiControllerTest()
        {
            _mockLogger = new Mock<ILogger<TApiController>>();
        }
    }
}
