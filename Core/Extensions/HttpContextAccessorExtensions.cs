using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static List<string> GetRoleClaims(this IHttpContextAccessor httpContextAccessor)
        {
            httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues jwt);
            string jwtString = jwt.ToString();
            if (jwtString.Length < 7)
            {
                throw new Exception("No token found");
            }
            jwtString = jwtString.Substring(7);
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtClaims = jwtHandler.ReadJwtToken(jwtString).Payload.Claims;
            var jwtRoleClaims = jwtClaims.Where<Claim>(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            List<string> roleClaims = new();
            foreach (var item in jwtRoleClaims)
            {
                roleClaims.Add(item.Value);
            }
            return roleClaims;
        }
    }
}
