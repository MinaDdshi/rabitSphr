using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class ProfileRepository : BaseRepository<Profile>
{
    #region [Constructor]

    public ProfileRepository(RedditMockupContext context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor) { }

    #endregion
}