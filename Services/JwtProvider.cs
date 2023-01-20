using NPOI.OpenXmlFormats.Wordprocessing;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JwtProvider : IJwtProvider
    {
        public string NewToken()
        {
            JwtSecurityToken token = new JwtSecurityToken();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string newToken = handler.WriteToken(token);

            return newToken;
        }
    }
}
