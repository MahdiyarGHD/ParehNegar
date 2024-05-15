 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.AspNetCore.Mvc.Filters;

namespace ParehNegar.Logics.Attributes;

public class CustomAuthorizeCheckAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public string[] ClaimTypes { get; set; }
    public CustomAuthorizeCheckAttribute(params string[] claimTypes)
    {
        ClaimTypes = claimTypes.Length > 0 ? claimTypes : ["Id", "CurrentLanguage"];
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user.Identity is { IsAuthenticated: false })
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var hasClaims = ClaimTypes.All(o => user.Claims.Any(x => x.Type == o));

        if (!hasClaims)
            context.Result = new UnauthorizedResult();
    }
}