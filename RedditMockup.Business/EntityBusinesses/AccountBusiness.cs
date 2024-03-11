using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.Helpers;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Business.EntityBusinesses;

public class AccountBusiness
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly UserRepository _userRepository;

    public AccountBusiness(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = unitOfWork.UserRepository!;
    }

    private async Task<bool> IsUsernameAndPasswordValidAsync(LoginDto login,
        CancellationToken cancellationToken = default)
    {
        SieveModel sieveModel = new()
        {
            Filters = $"Username=={login.Username!}"
        };

        var users = await _userRepository.LoadAllAsync(sieveModel, cancellationToken);

        if (users.Count == 0)
        {
            return false;
        }


        var password = await login.Password!.GetHashStringAsync();

        var isPasswordValid = password == users.Single().Password;

        return isPasswordValid;
    }

    private static bool IsSignedIn(HttpContext httpContext) =>
        httpContext.User.Identity is not null && httpContext.User.Identity.IsAuthenticated;

    private async Task<User?> LoadByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        SieveModel sieveModel = new() { Filters = $"Username=={username}" };

        var users = await _userRepository.LoadAllAsync(sieveModel, cancellationToken,
            users => users.Include(x => x.Person)
                .Include(x => x.Profile)
                .Include(x => x.Questions)
                .Include(x => x.Answers)
                .Include(x => x.UserRoles));

        if (users.Count == 0)
        {
            return null;
        }

        return users.Single();
    }

    public async Task<CustomResponse> LoginAsync(LoginDto login, HttpContext httpContext,
        CancellationToken cancellationToken = default)
    {
        if (IsSignedIn(httpContext))
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = "You are already signed in",
                HttpStatusCode = HttpStatusCode.BadRequest
            };
        }

        var isValid = await IsUsernameAndPasswordValidAsync(login, cancellationToken);

        if (!isValid)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = "Username and/or password not correct",
                HttpStatusCode = HttpStatusCode.BadRequest
            };
        }

        var user = await LoadByUsernameAsync(login.Username!, cancellationToken);

        var roles = await _unitOfWork.RoleRepository!.LoadByUserIdAsync(user!.Id, cancellationToken);

        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(role?.Title!, role?.Title!)));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        var properties = new AuthenticationProperties
        {
            IsPersistent = login.RememberMe
        };

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

        return new CustomResponse
        {
            IsSuccess = true,
            Message = "Successfully logged in",
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public static async Task<CustomResponse> LogoutAsync(HttpContext httpContext)
    {
        if (!IsSignedIn(httpContext))
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = "Already logged out",
                HttpStatusCode = HttpStatusCode.BadRequest
            };
        }

        await httpContext.SignOutAsync();

        return new CustomResponse
        {
            IsSuccess = true,
            Message = "Successfully logged out",
            HttpStatusCode = HttpStatusCode.OK
        };
    }
}