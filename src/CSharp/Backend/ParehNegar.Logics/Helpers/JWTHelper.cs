using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EasyMicroservices.ServiceContracts;
using Microsoft.IdentityModel.Tokens;
using ParehNegar.Domain.Contracts.Authentications;
using ParehNegar.Logics.Interfaces;
using ParehNegar.Logics.Logics;

namespace ParehNegar.Logics.Helpers;

public class JWTHelper(IUnitOfWork unitOfWork) : IJWTHelper
{
    public async Task<MessageContract<TokenResponseContract>> GenerateTokenWithClaims(List<ClaimContract> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(unitOfWork.GetValue("Authorization:JWT:Key"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.Select(x => new Claim(x.Name, x.Value)).ToArray()),
            Expires = DateTime.UtcNow.AddSeconds(int.Parse(unitOfWork.GetValue("Authorization:JWT:TokenExpireTimeInSeconds"))),
            Issuer = unitOfWork.GetValue("Authorization:JWT:Issuer"),
            Audience = unitOfWork.GetValue("Authorization:JWT:Audience"),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new TokenResponseContract
        {
            Token = tokenString,
        };
    }

    public async Task<MessageContract<TokenResponseContract>> EditTokenClaims(EditTokenClaimRequestContract request)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(request.Token);

        List<ClaimContract> newClaims = [];

        foreach (Claim claim in securityToken.Claims)
        {
            if (request.Claims.Any(o => o.Name == claim.Type))
                newClaims.Add(new ClaimContract
                {
                    Name = claim.Type,
                    Value = request.Claims.Where(o => o.Name == claim.Type).FirstOrDefault()?.Value
                });
            else
                newClaims.Add(new ClaimContract
                {
                    Name = claim.Type,
                    Value = claim.Value
                });

        }

        var token = await GenerateTokenWithClaims(newClaims);

        return token ? token : (FailedReasonType.Unknown, "An error has occured");
    }


    public async Task<ListMessageContract<ClaimContract>> GetClaimsFromToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

        List<ClaimContract> newClaims = securityToken.Claims.Select(o => new ClaimContract
        {
            Name = o.Type,
            Value = o.Value
        }).ToList();

        return newClaims;
    }
}