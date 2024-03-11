using RedditMockup.Business.Base;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;

namespace RedditMockup.Business.EntityBusinesses;

public class BookmarkBusiness : BaseBusiness<Bookmark>
{
    public BookmarkBusiness(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.BookmarkRepository!)
    {
    }
}
