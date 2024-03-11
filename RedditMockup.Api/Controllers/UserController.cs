using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

public class UserController : BaseController<User>
{
    public UserController(IBaseBusiness<User> business) : base(business)
    {
    }
}
