using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
          

            var authorizationToken = context.HttpContext.Request.Headers["Authorization"].ToString();
            string token = string.Empty;
            if (!string.IsNullOrEmpty(authorizationToken) && authorizationToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) && authorizationToken.Length > 7)
            {
                token = authorizationToken.Substring(7);
            }
            if (!string.IsNullOrEmpty(token))
            {
                //try
                //{
                //    GlobalContext.OperatorInfo = Operator.Instance.Current(token).Result;
                //    if (GlobalContext.OperatorInfo != null)
                //    {
                //        GlobalContext.OperatorInfo.CompanyId = GlobalContext.CompanyId;
                //    }
                //}
                //catch
                //{
                //}
            }
        }

    }
}
