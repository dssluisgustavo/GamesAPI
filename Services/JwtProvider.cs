using Domain;
using GamesAPI.Configurations;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NPOI.OpenXmlFormats.Wordprocessing;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions <JwtOptions> options)
        {
            _options = options.Value;
        }

        public string NewToken(User user)
        {
            string issuer = _options.Issuer;

            string audience = _options.Audience;

            // roxo
            Claim[] clains = new Claim[3]
            {
                new Claim (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim (JwtRegisteredClaimNames.Email, user.Email.ToString()),
                new Claim (JwtRegisteredClaimNames.UniqueName, user.Username.ToString())
            };

            DateTime dateTime = DateTime.Now.AddDays(1);

            // Vermelho e azul
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
            SigningCredentials crendential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(issuer, audience, clains, null, dateTime, crendential);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            

            string newToken = handler.WriteToken(token);

            return newToken;
        }
    }
}
