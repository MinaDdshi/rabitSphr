using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.DtoBusinesses;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Api.PublicControllers;

public class AnswerController : PublicBaseController<AnswerDto>
{
    #region [Fields]

    private readonly AnswerDtoBusiness _answerDtoBusiness;

    #endregion

    #region [Constructor]

    public AnswerController(IDtoBaseBusiness<AnswerDto> dtoBaseBusiness) : base(dtoBaseBusiness)
    {
        _answerDtoBusiness = (AnswerDtoBusiness)dtoBaseBusiness;
    }

    #endregion

    #region [Methods]

    [HttpGet]
    [Route("AnswersByQuestionId")]
    public async Task<CustomResponse<IEnumerable<AnswerDto>>> GetAnswersByQuestionIdAsync(int questionId, CancellationToken cancellationToken) =>
        await _answerDtoBusiness.GetAnswersByQuestionIdAsync(questionId, cancellationToken);

    [HttpGet]
    [Route("Votes")]
    public async Task<CustomResponse<IEnumerable<VoteDto>>> GetVotesByAnswerIdAsync(int answerId, CancellationToken cancellationToken) =>
        await _answerDtoBusiness.GetVotesByAnswerIdAsync(answerId, cancellationToken);

    [Authorize]
    [HttpPost]
    [Route("SubmitVote")]
    public async Task<CustomResponse> SubmitVoteAsync(int answerId, bool kind, CancellationToken cancellationToken) =>
        await _answerDtoBusiness.SubmitVoteAsync(answerId, kind, cancellationToken);

    #endregion
}

