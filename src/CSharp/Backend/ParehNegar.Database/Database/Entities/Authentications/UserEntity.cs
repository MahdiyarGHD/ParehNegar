using ParehNegar.Domain.BaseModels;

namespace ParehNegar.Database.Entities.Authentications;

public class UserEntity : FullAbilityIdSchema<long>
{
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? Location { get; set; }
    public string? Intro { get; set; }
    
    public ICollection<UserRoleEntity> UserRoles { get; set; }
}