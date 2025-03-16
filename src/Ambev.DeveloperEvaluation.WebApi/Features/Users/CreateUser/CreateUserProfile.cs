using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser
{
    /// <summary>
    /// AutoMapper profile for mapping between API and application models for user creation.
    /// </summary>
    public class CreateUserProfile : Profile
    {
        public CreateUserProfile()
        {
            CreateMap<CreateUserRequest, CreateUserCommand>();
            CreateMap<CreateUserAddressRequest, CreateUserAddress>();
            CreateMap<CreateUserGeolocationRequest, CreateUserGeolocation>();
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, CreateUserResult>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            CreateMap<Address, CreateUserAddressResult>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Zipcode))
                .ForMember(dest => dest.Geolocation, opt => opt.MapFrom(src => src.Geolocation));
            CreateMap<Geolocation, CreateUserGeolocationResult>()
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Long));
            CreateMap<CreateUserResult, CreateUserResponse>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            CreateMap<CreateUserAddressResult, CreateUserAddressResponse>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Zipcode))
                .ForMember(dest => dest.Geolocation, opt => opt.MapFrom(src => src.Geolocation));
            CreateMap<CreateUserGeolocationResult, CreateUserGeolocationResponse>()
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Long));
        }
    }
}