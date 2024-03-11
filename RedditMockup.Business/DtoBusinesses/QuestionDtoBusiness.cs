using System.Net;
using AutoMapper;
using RedditMockup.Business.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;

namespace RedditMockup.Business.DtoBusinesses;

public class QuestionDtoBusiness : DtoBaseBusiness<QuestionDto, Question>
{
    #region [Fields]

    private readonly QuestionBusiness _questionBusiness;

    private readonly IMapper _mapper;

    #endregion

    #region [Constructor]

    public QuestionDtoBusiness(IUnitOfWork unitOfWork, IBaseBusiness<Question> questionBusiness, IMapper mapper) : base(unitOfWork, unitOfWork.QuestionRepository!, mapper)
    {
        _questionBusiness = (QuestionBusiness)questionBusiness;

        _mapper = mapper;
    }

    #endregion

    #region [Methods]

    public async Task<CustomResponse> SubmitVoteAsync(int id, bool kind, CancellationToken cancellationToken = default) =>
        await _questionBusiness.SubmitVoteAsync(id, kind, cancellationToken);


    public async Task<CustomResponse<IEnumerable<VoteDto>>> GetVotesByQuestionIdAsync(int questionId, CancellationToken cancellationToken = default)
    {
        var votesResponse = await _questionBusiness.GetVotesByQuestionIdAsync(questionId, cancellationToken);

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
