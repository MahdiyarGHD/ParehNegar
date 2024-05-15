using System.Security.Claims;
using EasyMicroservices.ServiceContracts;
using Microsoft.Extensions.Configuration;
using ParehNegar.Database.Entities.Authentications;
using ParehNegar.Domain.Contracts.Authentications;
using ParehNegar.Logics.Logics;

namespace ParehNegar.Logics.Helpers;

public class IdentityHelper
{
        IUnitOfWork _unitOfWork;

        public IdentityHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<MessageContract<RegisterResponseContract>> Register(AddUserRequestContract request)
        {
            var userLogic = _unitOfWork.GetLongContractLogic<UserEntity, UserContract>();
            var user = await userLogic.GetByAsync(q => q.UserName.Equals(request.UserName, StringComparison.OrdinalIgnoreCase) || q.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase));
            if (user.IsSuccess)
                    return (FailedReasonType.Duplicate, user.Result.UserName.Equals(request.UserName, StringComparison.OrdinalIgnoreCase) ? $"کاربر با نام‌کاربری {request.UserName} درحال حاضر وجود دارد." : $"کاربر با ایمیل {request.Email} درحال حاضر وجود دارد.");
            var addedUserId = await userLogic.AddAsync(new UserContract
            {
                UserName = request.UserName,
                Password = request.Password,
                FullName = request.FullName,
                Email = request.Email
            }).AsCheckedResult(x => x.Result);

            return new RegisterResponseContract
            {
                UserId = addedUserId,
            };
        }

        public virtual async Task<long> Login(UserSummaryContract request)
        {
            var userLogic = _unitOfWork.GetLongContractLogic<UserEntity, UserContract>();
            var user = await userLogic.GetByAsync(q => (q.UserName.Equals(request.UserName, StringComparison.OrdinalIgnoreCase) || q.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)) && Equals(request.Password, q.Password)).AsCheckedResult(x => x.Result);
            return user.Id;
        }

        public virtual async Task<MessageContract<TokenResponseContract>> GenerateToken(UserClaimContract userClaim)
        {
            await Login(userClaim);

            var token = await _unitOfWork.GetJWTHelper().GenerateTokenWithClaims(userClaim.Claims);

            return new TokenResponseContract
            {
                Token = token.Result.Token
            };
        }

        // public Task<string> GetFullAccessPersonalAccessToken()
        // {
        //     var ownerPat = _unitOfWork.GetConfiguration().GetValue<string>("Authorization:FullAccessPAT");
        //     return GetFullAccessPersonalAccessToken(ownerPat);
        // }

        // public async Task<string> GetFullAccessPersonalAccessToken(string personalAccessToken)
        // {
        //     var user = await _unitOfWork.GetUserClient().GetUserByPersonalAccessTokenAsync(new Authentications.GeneratedServices.PersonalAccessTokenRequestContract()
        //     {
        //         Value = personalAccessToken
        //     }).AsCheckedResult(x => x.Result);
        //
        //     var roles = await _unitOfWork.GetRoleClient().GetRolesByUserIdAsync(new Authentications.GeneratedServices.GetByIdAndUniqueIdentityRequestContract
        //     {
        //         Id = user.Id,
        //         //UniqueIdentity = user.UniqueIdentity
        //     }).AsCheckedResult(x => x.Result);
        //
        //     List<ClaimContract> claims = new();
        //     var _claimManager = _unitOfWork.GetClaimManager();
        //     _claimManager.SetCurrentLanguage(_claimManager.CurrentLanguage, claims);
        //     if (!_claimManager.HasId())
        //     {
        //         _claimManager.SetId(user.Id, claims);
        //         _claimManager.SetUniqueIdentity(user.UniqueIdentity, claims);
        //         _claimManager.SetRole(roles.Select(x => new ClaimContract()
        //         {
        //             Name = ClaimTypes.Role,
        //             Value = x.Name
        //         }).ToList(), claims);
        //     }
        //
        //     var response = await _unitOfWork.GetIJWTManager()
        //         .GenerateTokenWithClaims(claims)
        //         .AsCheckedResult();
        //     return response.Token;
        // }
}