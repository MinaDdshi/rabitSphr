using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class UserRoleRepository : BaseRepository<UserRole>
{
    #region [Constructor]

    public UserRoleRepository(RedditMockupContext context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor) { }

    #endregion
}