using ParehNegar.Domain.BaseModels;

namespace ParehNegar.Database.Entities.Authentications;

public class UserRoleEntity : FullAbilityIdSchema<long>
{
    public long UserId { get; set; }
    public UserEntity User { get; set; }
    public long RoleId { get; set; }
    public RoleEntity Role { get; set; }
}