using RedditMockup.Business.Base;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
//using StackExchange.Redis;

namespace RedditMockup.Business.EntityBusinesses;

public class UserBusiness : BaseBusiness<User>
{
    #region [Redis Section]

    //private readonly IConnectionMultiplexer _connectionMultiplexer;

    //private readonly IDatabase _redisDb;


    /*
       public UserBusiness(IUnitOfWork unitOfWork, IMapper mapper, IConnectionMultiplexer connectionMultiplexer) :
            base(unitOfWork, unitOfWork.UserRepository!, mapper)
    {
        _userRepository = unitOfWork.UserRepository!;
        Mapper = mapper;
        _connectionMultiplexer = connectionMultiplexer;
        _redisDb = _connectionMultiplexer.GetDatabase();
    }

    */

    //var key = $"User {user.Id}";

    //var value = user.Username;

    //await _redisDb.StringSetAsync(key, value);

    #endregion

    #region [Constructor]

    public UserBusiness(IUnitOfWork unitOfWork) :
            base(unitOfWork, unitOfWork.UserRepository!)
    {
    }

    #endregion
}