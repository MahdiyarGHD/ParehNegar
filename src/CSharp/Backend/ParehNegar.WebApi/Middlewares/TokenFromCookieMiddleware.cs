using Microsoft.AspNetCore.Authorization;
using ParehNegar.Logics.Attributes;

namespace ParehNegar.WebApi.Middlewares
{
    public class TokenFromCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenFromCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader) &&
                    authHeader.Count != 0 && authHeader[0].StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                    return;

                if (httpContext.Request.Cookies.TryGetValue("token", out var token))
                    if (!string.IsNullOrWhiteSpace(token))
                        httpContext.Request.Headers.Add("Authorization", $"Bearer {token}");
            }
            finally
            {
                await _next.Invoke(httpContext);
            }
        }
    }
}