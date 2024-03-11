using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class BookmarkRepository : BaseRepository<Bookmark>
{
    public BookmarkRepository(RedditMockupContext context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor)
    {
    }

}
