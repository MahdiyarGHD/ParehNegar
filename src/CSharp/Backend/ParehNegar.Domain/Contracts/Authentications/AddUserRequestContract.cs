namespace ParehNegar.Domain.Contracts.Authentications;

public class AddUserRequestContract
{
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}