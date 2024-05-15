using ParehNegar.Domain.BaseModels;

namespace ParehNegar.Domain.Contracts.Authentications;

public class UserRoleContract : FullAbilityIdSchema<long>
{
    public long UserId { get; set; }
    public UserContract User { get; set; }
    public long RoleId { get; set; }
    public RoleContract Role { get; set; }
}