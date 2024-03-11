using RedditMockup.Common.Dtos;
//using RedditMockup.Common.ViewModels;
using RedditMockup.Model.Entities;
using Profile = AutoMapper.Profile;

namespace RedditMockup.Common.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        //CreateMap<User, UserViewModel>()
        //    .ForMember(destination => destination.PersonFullName,
        //        option =>
        //            option.MapFrom(source => source.Person!.FullName))
        //    .ForMember(destination => destination.Roles,
        //        option =>
        //            option.MapFrom(source => source.UserRoles!.Select(x => x.Role!.Title)))
        //    .ReverseMap();

        CreateMap<User, UserDto>()
            .ForMember(destination => destination.Name,
            option => option.MapFrom(source => source.Person!.Name))
            .ForMember(destination => destination.Family,
            option => option.MapFrom(source => source.Person!.Family))
            .ForMember(destination => destination.Roles,
                option =>
                    option.MapFrom(source => source.UserRoles!.Select(x => x.Role!.Title)))
            .ReverseMap();
    }
}