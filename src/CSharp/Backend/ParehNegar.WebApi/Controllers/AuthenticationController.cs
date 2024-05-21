using System.Security.Claims;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParehNegar.Database.Database.Entities.Contents;
using ParehNegar.Database.Entities.Authentications;
using ParehNegar.Domain.Contracts.Authentications;
using ParehNegar.Domain.Contracts.Contents;
using ParehNegar.Logics.Attributes;
using ParehNegar.Logics.Logics;

namespace ParehNegar.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthenticationController(IUnitOfWork unitOfWork) : ControllerBase 
{
    private void SetCookie(string key, string value)
    {
        if (!bool.TryParse(unitOfWork.GetValue("HasSSL"), out bool hasSSL))
            return;
        if (hasSSL)
        {
            var cookieOptions = new CookieOptions
            {
                SameSite = SameSiteMode.None,
                HttpOnly = true,
                Secure = true
            };
            Response.Cookies.Append(key, value, cookieOptions);
        }
        else
            Response.Cookies.Append(key, value);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<MessageContract<RegisterResponseContract>> Register(AddUserRequestContract request)
    {
        if (unitOfWork.GetClaimManager().HasId())
            return (FailedReasonType.AccessDenied, "You are already logined.");
        
        var identityHelper = unitOfWork.GetIdentityHelper();
        return await identityHelper.Register(request);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<MessageContract<AppInitResponseContract>> AppInit(AppInitRequestContract request)
    {
        var languageLogic = unitOfWork.GetLongContractLogic<LanguageEntity, LanguageContract>();

        await languageLogic.GetByAsync(
            q => q.Name.Equals(request.Language, StringComparison.OrdinalIgnoreCase)
        ).AsCheckedResult(x => x.Result);

        List<ClaimContract> claims = [];
        
        var claimManager = unitOfWork.GetClaimManager();
        claimManager.SetCurrentLanguage(request.Language, claims);
        if (claimManager.HasId())
        {
            claimManager.SetId(claimManager.Id, claims);
            claimManager.SetRole(claimManager.Role.Select(o => new ClaimContract
            {
                Name = ClaimTypes.Name,
                Value = o
            }).ToList(), claims);
        }
        var tokenResponse = await unitOfWork.GetJWTHelper().GenerateTokenWithClaims(claims);

        SetCookie("token", tokenResponse.Result.Token);

        return new AppInitResponseContract { IsLogined = claimManager.HasId(), Token = tokenResponse.Result.Token };
    }
    
    [HttpPost]
    [AppInitCheck]
    public async Task<MessageContract<LoginResponseContract>> Login(UserSummaryContract request)
    {
        if (unitOfWork.GetClaimManager().HasId())
            return (FailedReasonType.AccessDenied, "You are already logined.");
        
        var identityHelper = unitOfWork.GetIdentityHelper();
        var response = await identityHelper.Login(request);

        var user = await unitOfWork.GetLongContractLogic<UserEntity, UserContract>()
            .GetByIdAsync(response, e => e.Include(q => q.UserRoles).ThenInclude(q => q.Role))
            .AsCheckedResult(x => x.Result);

        List<ClaimContract> claims = [];

        var claimManager = unitOfWork.GetClaimManager();
        
        claimManager.SetCurrentLanguage(claimManager.CurrentLanguage, claims);
        if (!claimManager.HasId())
        {
            claimManager.SetId(user.Id, claims);
            claimManager.SetRole(user.UserRoles.Select(x => new ClaimContract
            {
                Name = ClaimTypes.Role,
                Value = x.Role.Slug
            }).ToList(), claims);
        }
        var tokenResponse = await unitOfWork.GetJWTHelper().GenerateTokenWithClaims(claims);

        SetCookie("token", tokenResponse.Result.Token);

        return new LoginResponseContract
        {
            UserId = response,
            Token = tokenResponse.Result.Token
        };
    }
    
    [HttpPost]
    [CustomAuthorizeCheck]
    public async Task<MessageContract<TokenResponseContract>> Logout()
    {
        List<ClaimContract> claims = [];
        
        var claimManager = unitOfWork.GetClaimManager();
        claimManager.SetCurrentLanguage(claimManager.CurrentLanguage, claims);
        
        var tokenResponse = await unitOfWork.GetJWTHelper().GenerateTokenWithClaims(claims);
        SetCookie("token", tokenResponse.Result.Token);

        return new TokenResponseContract
        {
            Token = tokenResponse.Result.Token
        };
    }
}