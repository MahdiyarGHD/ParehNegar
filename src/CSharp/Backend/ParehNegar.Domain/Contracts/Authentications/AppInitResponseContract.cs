namespace ParehNegar.Domain.Contracts.Authentications;

public class AppInitResponseContract : TokenResponseContract
{
    public bool IsLogined { get; set; }
}