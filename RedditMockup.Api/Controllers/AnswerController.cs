using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

public class AnswerController : BaseController<Answer>
{
    #region [Fields]

    private readonly AnswerBusiness _business;

    #endregion

    #region [Constructor]

    public AnswerController(IBaseBusiness<Answer> business) : base(business)
    {
        _business = (AnswerBusiness)business;
    }

    #endregion

    #region [Methods]

    [HttpGet]
    [Route("AnswersByQuestionId")]
    public async Task<CustomResponse<IEnumerable<Answer>>> GetAnswersByQuestionIdAsync(int questionId, CancellationToken cancellationToken) =>
        await _business.GetAnswersByQuestionIdAsync(questionId, cancellationToken);

    [HttpGet]
    [Route("Votes")]
    public async Task<CustomResponse<IEnumerable<AnswerVote>>> GetVotesByAnswerIdAsync(int answerId, CancellationToken cancellationToken) =>
        await _business.GetVotesByAnswerIdAsync(answerId, cancellationToken);

    [HttpPost]
    [Route("SubmitVote")]
    public async Task<CustomResponse> SubmitVoteAsync(int answerId, bool kind, CancellationToken cancellationToken) =>
        await _business.SubmitVoteAsync(answerId, kind, cancellationToken);

    #endregion
}