using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;
using Profile = AutoMapper.Profile;

namespace RedditMockup.Common.Profiles;

public class AnswerProfile : Profile
{
    public AnswerProfile() =>
        CreateMap<Answer, AnswerDto>()
        .ReverseMap();
}