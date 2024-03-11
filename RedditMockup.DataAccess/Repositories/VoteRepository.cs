using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class AnswerVoteRepository : BaseRepository<AnswerVote>
{
    #region [Constructor]

    public AnswerVoteRepository(RedditMockupContext context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor) { }

    #endregion

}

public class QuestionVoteRepository : BaseRepository<QuestionVote>
{
    #region [Constructor]

    public QuestionVoteRepository(RedditMockupContext context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor) { }

    #endregion
}