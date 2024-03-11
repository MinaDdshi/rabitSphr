using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountBusiness _accountBusiness;

    public AccountController(AccountBusiness accountBusiness) =>
        _accountBusiness = accountBusiness;

    [Authorize]
    [HttpPost]
    [Route("Logout")]
    public async Task<CustomResponse> Logout() =>
        await AccountBusiness.LogoutAsync(HttpContext);

    [HttpPost]
    [Route("Login")]
    public async Task<CustomResponse> LoginAsync(LoginDto login, CancellationToken cancellationToken) =>
        await _accountBusiness.LoginAsync(login, HttpContext, cancellationToken);

}