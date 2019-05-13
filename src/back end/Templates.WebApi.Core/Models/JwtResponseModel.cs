using System;
using System.Collections.Generic;
using System.Text;

namespace Templates.WebApi.Core.Models
{

    public class JwtResponseModel
    {
        public bool Status { get; set; }

        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }
    }
}
