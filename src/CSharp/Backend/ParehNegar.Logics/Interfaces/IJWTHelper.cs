using EasyMicroservices.ServiceContracts;
using ParehNegar.Domain.Contracts.Authentications;

namespace ParehNegar.Logics.Interfaces;

public interface IJWTHelper
{
    Task<ListMessageContract<ClaimContract>> GetClaimsFromToken(string token);
    Task<MessageContract<TokenResponseContract>> EditTokenClaims(EditTokenClaimRequestContract request);
    Task<MessageContract<TokenResponseContract>> GenerateTokenWithClaims(List<ClaimContract> claims);
}