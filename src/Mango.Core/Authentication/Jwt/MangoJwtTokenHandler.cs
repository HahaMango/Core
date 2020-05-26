﻿using Mango.Core.EntityFramework.Abstractions;
using Mango.Core.EntityFramework.BaseEntity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Core.Authentication.Jwt
{
    /// <summary>
    /// Jwt验证，token颁发，token过期等处理器
    /// </summary>
    public class MangoJwtTokenHandler
    {
        /// <summary>
        /// jwt配置
        /// </summary>
        public MangoJwtOptions Options { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MangoJwtTokenHandler() 
        {
            Options = new MangoJwtOptions();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public MangoJwtTokenHandler(MangoJwtOptions options)
        {
            Options = options;
        }

        /// <summary>
        /// 颁发令牌
        /// </summary>
        /// <typeparam name="TUser">用户实体类，实现IBaseEntity</typeparam>
        /// <typeparam name="TKey">用户Id类型</typeparam>
        /// <param name="user"></param>
        /// <param name="otherClaims">额外添加的claims</param>
        /// <returns></returns>
        public string IssuedToken<TUser,TKey>(TUser user, params Claim[] otherClaims)
            where TUser : IBaseEntity<TKey>
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            #region 如果实体类有UserName字段
            var userType = user.GetType();
            var userProp = userType.GetProperty("UserName", typeof(string));
            if (userProp != null)
            {
                var userName = (string)userProp.GetValue(user);
                claims.Add(new Claim(ClaimTypes.Name, userName));
            }
            #endregion

            claims.AddRange(otherClaims);

            var sec = Options.ExpiresSec.HasValue ? Options.ExpiresSec.Value : 604800;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: Options.Issuer,
                audience: Options.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(sec),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 颁发令牌
        /// </summary>
        /// <typeparam name="TUser">用户实体类，实现IBaseEntity</typeparam>
        /// <typeparam name="TKey">用户Id类型</typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        public string IssuedToken<TUser, TKey>(TUser user)
            where TUser : IBaseEntity<TKey>
        {
            return IssuedToken<TUser, TKey>(user, new Claim[0]);
        }

        /// <summary>
        /// 颁发令牌
        /// </summary>
        /// <typeparam name="TUser">用户实体类，实现Entity</typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        public string IssuedToken<TUser>(TUser user)
            where TUser : Entity
        {
            return IssuedToken<TUser, long>(user);
        }
    }
}