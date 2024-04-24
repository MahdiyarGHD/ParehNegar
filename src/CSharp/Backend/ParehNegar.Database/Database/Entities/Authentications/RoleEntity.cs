using ParehNegar.Domain.BaseModels;

namespace ParehNegar.Database.Entities.Authentications;

public class RoleEntity : FullAbilityIdSchema<long>
{
    public string Title { get; set; }
    public string Slug { get; set; }
    
    public ICollection<UserRoleEntity> UserRoles { get; set; }
}