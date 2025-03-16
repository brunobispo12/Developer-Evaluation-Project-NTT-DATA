using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
    /// <summary>
    /// AutoMapper profile for mapping between the application update user command/results and the API update user response.
    /// </summary>
    public class UpdateUserProfile : Profile
    {
        public UpdateUserProfile()
        {
            // Map API request to Application command.
            CreateMap<UpdateUserRequest, UpdateUserCommand>();

            // Configure mapping for nested update address and geolocation.
            CreateMap<UpdateAddressRequest, UpdateUserAddress>();
            CreateMap<UpdateGeolocationRequest, UpdateUserGeolocation>();

            // Map from the application UpdateUserCommand to the domain User.
            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                // Map the Name property by constructing a new Name from FirstName and LastName.
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name
                {
                    Firstname = src.FirstName,
                    Lastname = src.LastName
                }))
                // For Address, use the mapping defined above (from UpdateAddressRequest to UpdateUserAddress)
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // Map from the domain User to the application UpdateUserResult.
            CreateMap<User, UpdateUserResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                // Flatten the nested Name property.
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.Firstname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.Lastname))
                // Let AutoMapper map Address using the mapping defined below.
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // Map from the domain Address to the application UpdateUserAddressResult.
            CreateMap<Address, UpdateUserAddressResult>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Zipcode))
                .ForMember(dest => dest.Geolocation, opt => opt.MapFrom(src => src.Geolocation));

            // Map from the domain Geolocation to the application UpdateUserGeolocationResult.
            CreateMap<Geolocation, UpdateUserGeolocationResult>()
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Long));

            // Map from the application UpdateUserResult to the API response.
            CreateMap<UpdateUserResult, UpdateUserResponse>();

            // Map from the application UpdateUserAddressResult to the API's UpdateAddressResponse.
            CreateMap<UpdateUserAddressResult, UpdateAddressResponse>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Zipcode))
                .ForMember(dest => dest.Geolocation, opt => opt.MapFrom(src => src.Geolocation));

            // Map from the application UpdateUserGeolocationResult to the API's UpdateGeolocationResponse.
            CreateMap<UpdateUserGeolocationResult, UpdateGeolocationResponse>()
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Long));
        }
    }
}