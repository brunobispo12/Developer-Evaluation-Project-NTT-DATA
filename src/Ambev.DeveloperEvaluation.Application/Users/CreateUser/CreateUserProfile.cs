using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser
{
    /// <summary>
    /// AutoMapper profile for mapping between application create user commands/results and the domain entity.
    /// </summary>
    public class CreateUserProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateUser operation.
        /// </summary>
        public CreateUserProfile()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name
                {
                    Firstname = src.FirstName,
                    Lastname = src.LastName
                }))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    City = src.Address.City,
                    Street = src.Address.Street,
                    Number = src.Address.Number,
                    Zipcode = src.Address.Zipcode,
                    Geolocation = new Geolocation
                    {
                        Lat = src.Address.Geolocation.Lat,
                        Long = src.Address.Geolocation.Long
                    }
                }));
            CreateMap<User, CreateUserResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.Firstname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
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
        }
    }
}