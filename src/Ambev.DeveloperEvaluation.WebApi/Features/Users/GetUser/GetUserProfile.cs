using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser
{
    /// <summary>
    /// Profile for mapping between GetUserResult and GetUserResponse.
    /// </summary>
    public class GetUserProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserProfile"/> class.
        /// </summary>
        public GetUserProfile()
        {
            CreateMap<GetUserResult, GetUserResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name.Firstname} {src.Name.Lastname}"));
        }
    }
}