namespace ParehNegar.Domain.Contracts.Authentications;

public class EditTokenClaimRequestContract
{
    public string Token { get; set; }
    public List<ClaimContract> Claims { get; set; }
}