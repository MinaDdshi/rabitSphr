using RedditMockup.Business.Base;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

public class BookmarkController : BaseBusiness<Bookmark>
{
    public BookmarkController(IUnitOfWork unitOfWork, IBaseRepository<Bookmark> repository) : base(unitOfWork, repository)
    {
    }
}

