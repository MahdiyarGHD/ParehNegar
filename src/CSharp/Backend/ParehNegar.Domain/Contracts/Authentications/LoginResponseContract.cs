namespace ParehNegar.Domain.Contracts.Authentications
{
    public class LoginResponseContract : TokenResponseContract
    {
        public long UserId { get; set; }
    }
}