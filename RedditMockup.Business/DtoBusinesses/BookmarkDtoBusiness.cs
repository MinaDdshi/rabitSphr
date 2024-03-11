using AutoMapper;
using RedditMockup.Business.Base;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;

namespace RedditMockup.Business.DtoBusinesses;

public class BookmarkDtoBusiness : DtoBaseBusiness<BookmarkDto, Bookmark>
{
    public BookmarkDtoBusiness(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.BookmarkRepository!, mapper)
    {
    }
}

