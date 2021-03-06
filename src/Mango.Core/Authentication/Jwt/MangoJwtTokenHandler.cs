﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Core.Authentication.Jwt
{
    /// <summary>
    /// jwt token颁发处理器（过期修改密码等操作由客户端放弃密钥）
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
        /// <param name="otherClaims">额外添加的claims claimType为'aud'可追加aud字段。</param>
        /// <param name="audience">接受者</param>
        /// <param name="issuer">颁发机构</param>
        /// <returns></returns>
        public string IssuedToken<TUser,TKey>(TUser user, string issuer = null,string audience = null, params Claim[] otherClaims)
        {
            TKey a = default;
            #region 获取实体id
            var userType = user.GetType();
            var userIdProp = userType.GetProperty("Id", a.GetType());
            if(userIdProp == null)
            {
                throw new InvalidOperationException("实体缺少ID属性");
            }
            var userId = userIdProp.GetValue(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userId.ToString())
            };
            #endregion

            #region 如果实体类有UserName字段
            var userProp = userType.GetProperty("UserName", typeof(string));
            if (userProp != null)
            {
                var userName = (string)userProp.GetValue(user);
                claims.Add(new Claim(ClaimTypes.Name, userName));
            }
            #endregion

            #region 如果实体类有Role字段
            var roleProp = userType.GetProperty("Role", typeof(string));
            if(roleProp != null)
            {
                var role = (string)userProp.GetValue(user);
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            #endregion

            claims.AddRange(otherClaims);

            var sec = Options.ExpiresSec.HasValue ? Options.ExpiresSec.Value : 604800;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer ?? Options.DefalutIssuer,
                audience: audience ?? Options.DefalutAudience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(sec),
                signingCredentials: creds);
            var aud = new Claim("aud", "");

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 颁发令牌
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="user"></param>
        /// <param name="otherClaims"></param>
        /// <returns></returns>
        public string IssuedToken<TUser, TKey>(TUser user,params Claim[] otherClaims)
        {
            return IssuedToken<TUser, TKey>(user: user, issuer: null, audience: null, otherClaims: otherClaims);
        }

        /// <summary>
        /// 颁发令牌
        /// </summary>
        /// <typeparam name="TUser">用户实体类，实现IBaseEntity</typeparam>
        /// <typeparam name="TKey">用户Id类型</typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        public string IssuedToken<TUser, TKey>(TUser user)
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
        {
            return IssuedToken<TUser, long>(user);
        }
    }
}
