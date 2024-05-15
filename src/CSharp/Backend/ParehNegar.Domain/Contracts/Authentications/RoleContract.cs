using ParehNegar.Domain.BaseModels;

namespace ParehNegar.Domain.Contracts.Authentications;

public class RoleContract : FullAbilityIdSchema<long>
{
    public string Title { get; set; }
    public string Slug { get; set; }
}