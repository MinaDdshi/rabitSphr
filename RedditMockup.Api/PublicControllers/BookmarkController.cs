using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Api.PublicControllers;

public class BookmarkController : PublicBaseController<BookmarkDto>
{
    public BookmarkController(IDtoBaseBusiness<BookmarkDto> dtoBaseBusiness) : base(dtoBaseBusiness)
    {
    }
}

