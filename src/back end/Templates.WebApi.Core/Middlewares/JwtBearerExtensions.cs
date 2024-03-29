﻿using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Templates.Common;
using Templates.WebApi.Core.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JwtBearerExtensions
    {
        #region 从配置中读取参数值
        public const string Config_Root_Name = "JwtBearer";
        public const string Issuer_Name = "Issuer";
        public const string Audience_Name = "Audience";
        public const string Secret_Key_Name = "SecretKey";
        public const string Expiration_Name = "ExpiresIn";
        #endregion
        private const string Scheme = JwtBearerDefaults.AuthenticationScheme;

        private static readonly Encoding _encoding = Encoding.UTF8;

        /// <summary>
        /// 添加JwtBearer认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            Ensure.NotNull(services, nameof(services));
            Ensure.NotNull(configuration, nameof(configuration));

            var root = configuration.GetSection(Config_Root_Name);
            var issuer = root[Issuer_Name];
            var audience = root[Audience_Name];
            var secretKey = root[Secret_Key_Name];
            var issuerSigningKey = new SymmetricSecurityKey(_encoding.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters()
            {
                NameClaimType = JwtClaimTypes.Name,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = issuerSigningKey,
                //默认是5分钟，过期时间 = ClockSkew + exp
                ClockSkew = TimeSpan.FromSeconds(0)
            };

            services.AddAuthentication(Scheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return ctx.Response.WriteAsync("Token无效");
                    }
                };
            });
        }

        /// <summary>
        /// 转为Jwt响应数据格式
        /// </summary>
        /// <param name="model"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static JwtResponseModel ToJwtResponse(this TokenModel model, IConfiguration configuration)
        {
            Ensure.NotNull(model, nameof(model));
            Ensure.NotNull(configuration, nameof(configuration));

            var now = DateTime.Now;
            var root = configuration.GetSection(Config_Root_Name);
            var issuer = root[Issuer_Name];
            var audience = root[Audience_Name];
            var secretKey = root[Secret_Key_Name];
            var issuerSigningKey = new SymmetricSecurityKey(_encoding.GetBytes(secretKey));
            var expiresIn = TimeSpan.Parse(root[Expiration_Name]);
            var expires = now.Add(expiresIn);
           
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Id, model.UserId),
                    new Claim(JwtClaimTypes.Name, model.UserName),
                }),
                Audience = audience,
                IssuedAt = now,
                NotBefore = now,
                Expires = expires,
                SigningCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtResponse = new JwtResponseModel()
            {
                Status = true,
                AccessToken = new JwtSecurityTokenHandler().CreateEncodedJwt(tokenDescriptor),
                ExpiresIn = (int)expiresIn.TotalSeconds,
                TokenType = Scheme
            };

            return jwtResponse;
        }
    }

}
