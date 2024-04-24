using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ParehNegar.Domain.Contracts.Authentications;

namespace ParehNegar.Logics.Managers;

public class ClaimManager
{
    private List<Claim> claims = [];

    public ClaimManager(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;

        var token = _httpContext.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            claims = jwtToken.Claims.ToList();
        }
    }

    IHttpContextAccessor _httpContext;

    public bool HasId()
    {
        return claims.Any(x => x.Type == "Id");
    }

    public long Id
    {
        get
        {
            return long.Parse(claims.FirstOrDefault(x => x.Type == "Id")?.Value);
        }
    }

    public string CurrentLanguage
    {
        get
        {
            return claims.FirstOrDefault(x => x.Type == "CurrentLanguage")?.Value;
        }
    }

    public List<ClaimContract> SetCurrentLanguage(string value, List<ClaimContract> claims = default)
    {
        claims ??= [];
        if (value != null)
            claims.Add(new ClaimContract
            {
                Name = "CurrentLanguage",
                Value = value
            });

        return claims;
    }

    public List<ClaimContract> SetId(long? value, List<ClaimContract> claims = default)
    {
        claims ??= [];
        if (value.HasValue)
            claims.Add(new ClaimContract
            {
                Name = "Id",
                Value = value.Value.ToString()
            });

        return claims;
    }
}