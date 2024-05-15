using ParehNegar.Domain.BaseModels;

namespace ParehNegar.Domain.Contracts.Authentications
{
    public class UserContract : FullAbilityIdSchema<long>
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<UserRoleContract> UserRoles { get; set; }
        public string? Location { get; set; }
        public string? Intro { get; set; }
    }
}