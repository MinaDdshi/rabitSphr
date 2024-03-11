using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

public class QuestionController : BaseController<Question>
{
    private readonly QuestionBusiness _business;

    public QuestionController(IBaseBusiness<Question> business) : base(business)
    {
        _business = (QuestionBusiness)business;
    }

    [HttpGet]
    [Route("Votes")]
    public async Task<CustomResponse<IEnumerable<QuestionVote>>> GetVotesByQuestionIdAsync(int questionId, CancellationToken cancellationToken) =>
         await _business.GetVotesByQuestionIdAsync(questionId, cancellationToken);


    [HttpPost]
    [Route("SubmitVote")]
    public async Task<CustomResponse> SubmitVoteAsync(int questionId, bool kind, CancellationToken cancellationToken) =>
        await _business.SubmitVoteAsync(questionId, kind, cancellationToken);
}