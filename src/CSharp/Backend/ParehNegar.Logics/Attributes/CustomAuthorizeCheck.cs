 using Azure;
 using Azure.Core;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.AspNetCore.Mvc.Filters;

namespace ParehNegar.Logics.Attributes;

public class CustomAuthorizeCheckAttribute : AuthorizeAttribute
{
    public string[] ClaimTypes { get; set; }
    public CustomAuthorizeCheckAttribute(params string[] claimTypes)
    {
        ClaimTypes = claimTypes.Length > 0 ? claimTypes : ["Id", "CurrentLanguage"];
    }
}