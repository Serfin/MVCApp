using AutoMapper;
using ExileRota.Core.Domain;
using MVCApp.Core.Domain;
using MVCApp.Infrastructure.DTO;

namespace MVCApp.Presentation.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
            => new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserDto>();
                config.CreateMap<UserDto, User>();
                config.CreateMap<Rotation, RotationDto>();
            }).CreateMapper();
    }
}