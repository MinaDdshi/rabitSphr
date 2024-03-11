using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class QuestionRepository : BaseRepository<Question>
{
    #region [Constructor]

    public QuestionRepository(RedditMockupContext context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor) { }

    #endregion

}