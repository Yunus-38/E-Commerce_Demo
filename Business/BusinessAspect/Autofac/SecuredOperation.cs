using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Business.BusinessAspect.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            //This, for some reason does not work.
            //var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

            //This is the roundabout method I used to manually access the claims. I will probably write a method for all this later.
            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues jwt);
            string jwtString = jwt.ToString().Substring(7);
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtClaims = jwtHandler.ReadJwtToken(jwtString).Payload.Claims;
            var jwtRoleClaims = jwtClaims.Where<Claim>(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            List<string> roleClaims = new();
            foreach (var item in jwtRoleClaims)
            {
                roleClaims.Add(item.Value);
            }
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception("Authorization Denied");
        }
    }
}
