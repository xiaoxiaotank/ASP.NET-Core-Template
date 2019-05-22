using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Templates.WebApi.Core.Controllers;

namespace Templates.WebApi.Controllers
{
    /// <summary>
    /// 所有与具体业务无关的api均放在此处
    /// </summary>
    public class SharedController : ApiController
    {
        [HttpDelete("deleteFiles")]
        public ActionResult DeleteFiles([FromBody]IEnumerable<string> urls, [FromServices]ILogger<ApiController> logger)
        {
            return Ok();
        }

    }
}