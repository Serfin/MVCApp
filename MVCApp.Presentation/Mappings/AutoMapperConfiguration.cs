using AutoMapper;
using MVCApp.Core.Domain;
using MVCApp.Infrastructure.ViewModels;
using MVCApp.Presentation.Models;

namespace MVCApp.Presentation.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
            => new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserViewModel>(); 

            }).CreateMapper();
    }
}