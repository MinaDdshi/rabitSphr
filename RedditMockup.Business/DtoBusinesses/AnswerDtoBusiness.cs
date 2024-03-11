using AutoMapper;
using RedditMockup.Business.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;
using System.Net;

namespace RedditMockup.Business.DtoBusinesses;

public class AnswerDtoBusiness : DtoBaseBusiness<AnswerDto, Answer>
{
    private readonly IMapper _mapper;

    private readonly AnswerBusiness _answerBusiness;

    #region [Constructor]

    public AnswerDtoBusiness(IUnitOfWork unitOfWork, IBaseBusiness<Answer> answerBusiness, IMapper mapper) : base(unitOfWork, unitOfWork.AnswerRepository!, mapper)
    {
        _mapper = mapper;
        _answerBusiness = (AnswerBusiness)answerBusiness;
    }

    #endregion

    #region [Methods]

    public async Task<CustomResponse<IEnumerable<AnswerDto>>> GetAnswersByQuestionIdAsync(int questionId, CancellationToken cancellationToken = default)
    {
        var answersResponse = await _answerBusiness.GetAnswersByQuestionIdAsync(questionId, cancellationToken);

        if (!answersResponse.IsSuccess)
        {
            return new()
            {
                IsSuccess = answersResponse.IsSuccess,
                Message = answersResponse.Message,
                HttpStatusCode = answersResponse.HttpStatusCode
            };
        }

        var answerDtos = _mapper.Map<IEnumerable<AnswerDto>>(answersResponse.Data);

        return new()
        {
            Data = answerDtos,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse> SubmitVoteAsync(int answerId, bool kind, CancellationToken cancellationToken = default) =>
        await _answerBusiness.SubmitVoteAsync(answerId, kind, cancellationToken);

    public async Task<CustomResponse<IEnumerable<VoteDto>>> GetVotesByAnswerIdAsync(int answerId, CancellationToken cancellationToken = default)
    {
        var votesResponse = await _answerBusiness.GetVotesByAnswerIdAsync(answerId, cancellationToken);

        if (!votesResponse.IsSuccess)
        {
            return new()
            {
                IsSuccess = votesResponse.IsSuccess,
                Message = votesResponse.Message,
                HttpStatusCode = votesResponse.HttpStatusCode
            };
        }

        var voteDtos = _mapper.Map<IEnumerable<VoteDto>>(votesResponse.Data);

        return new()
        {
            Data = voteDtos,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    #endregion
}
