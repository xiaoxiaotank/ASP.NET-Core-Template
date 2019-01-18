using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Templates.WebAPI.Attributes.Filters
{
    public class MessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new Exception();
            return base.SendAsync(request, cancellationToken);
        }
    }
}
