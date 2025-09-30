using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OyoAgro.DataAccess.Layer.Models;

namespace OyoAgro.Api.Authorizations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute()
        {

        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
          

            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
            string token = string.Empty;
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                //token = authorizationToken.Substring(7);
                context.Result = new UnauthorizedResult();
                return;
            }
            token = authorizationHeader.Substring("Bearer ".Length).Trim();
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            using (var db = new AppDbContext())
            {
                var session = db.Useraccounts
                    .FirstOrDefault(s => s.Apitoken == token && s.Isactive == true);

                if (session == null)
                {
                    context.Result = new UnauthorizedResult(); // token not found or invalid
                    return;
                }

            }


        }

    }
}
