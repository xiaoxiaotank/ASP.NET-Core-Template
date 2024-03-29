﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Templates.WebApi.Core.Models
{
    /// <summary>
    /// 验证令牌
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        public TokenModel(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
