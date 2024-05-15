namespace ParehNegar.Domain.Contracts.Authentications;

public class UserClaimContract : UserSummaryContract
{
    public List<ClaimContract> Claims { get; set; }
}