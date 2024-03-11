using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.DtoBusinesses;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Api.PublicControllers;

public class QuestionController : PublicBaseController<QuestionDto>
{
    #region [Fields]

    private readonly QuestionDtoBusiness _questionDtoBusiness;

    #endregion

    #region [Constructor]

    public QuestionController(IDtoBaseBusiness<QuestionDto> questionDtoBaseBusiness) : base(questionDtoBaseBusiness)
    {
        _questionDtoBusiness = (QuestionDtoBusiness)questionDtoBaseBusiness;
    }

    #endregion

    [HttpGet]
    [Route("QuestionVotes")]
    public async Task<CustomResponse<IEnumerable<VoteDto>>> GetVotesByQuestionIdAsync(int questionId, CancellationToken cancellationToken) =>
       await _questionDtoBusiness.GetVotesByQuestionIdAsync(questionId, cancellationToken);

    [Authorize]
    [HttpPost]
    [Route("SubmitVote")]
    public async Task<CustomResponse?> SubmitVoteAsync(int questionId, bool kind, CancellationToken cancellationToken) =>
        await _questionDtoBusiness.SubmitVoteAsync(questionId, kind, cancellationToken);
}

