using AutoMapper;
using ExileRota.Core.Domain;
using MVCApp.Core.Domain;
using MVCApp.Infrastructure.ViewModels;

namespace MVCApp.Presentation.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
            => new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserViewModel>();
                config.CreateMap<UserViewModel, User>();
                config.CreateMap<Rotation, RotationViewModel>();
            }).CreateMapper();
    }
}