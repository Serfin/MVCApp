using AutoMapper;
using MVCApp.Common.ViewModels;
using MVCApp.Core.Domain;

namespace MVCApp.Common.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
            => new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserViewModel>()
                    .ReverseMap();

                config.CreateMap<Rotation, RotationViewModel>()
                    .ReverseMap();

            }).CreateMapper();
    }
}