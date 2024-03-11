using System.Net;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RedditMockup.Business.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.Helpers;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Business.DtoBusinesses;

public class UserDtoBusiness : DtoBaseBusiness<UserDto, User>
{
    #region [Fields]

    private readonly IMapper _mapper;

    private readonly UserBusiness _userBusiness;

    #endregion

    #region [Constructor]

    public UserDtoBusiness(IUnitOfWork unitOfWork, IBaseBusiness<User> userBusiness, IMapper mapper) : base(unitOfWork, unitOfWork.UserRepository!, mapper)
    {
        _mapper = mapper;

        _userBusiness = (UserBusiness)userBusiness;
    }

    #endregion

    #region [Methods]

    private async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken = default)
    {
        SieveModel sieveModel = new()
        {
            Filters = $"Username=={username}"
        };

        var usersResponse = await LoadAllAsync(sieveModel, cancellationToken);

        if (usersResponse.Data.IsNullOrEmpty())
        {
            return false;
        }

        return true;
    }

    public override async Task<CustomResponse<UserDto>> CreateAsync(UserDto dto,
        CancellationToken cancellationToken = default)
    {
        var isUsernameValid = !await UsernameExistsAsync(dto.Username!, cancellationToken);

        if (!isUsernameValid)
        {
            return new()
            {
                IsSuccess = false,
                Message = $"{dto.Username} is already used, try another one",
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        dto.Password = await dto.Password!.GetHashStringAsync();

        var user = _mapper.Map<User>(dto);

        user.Profile = new()
        {
            UserId = user.Id
        };

        var userRole = new UserRole
        {
            UserId = user.Id,
            RoleId = 2
        };

        user.UserRoles!.Add(userRole);

        user = await _userBusiness.CreateAsync(user, cancellationToken);

        if (user is null)
        {
            return new()
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.InternalServerError
            };
        }

        var userDto = _mapper.Map<UserDto>(user);

        return new()
        {
            Data = userDto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.Created
        };

    }

    #endregion
}
