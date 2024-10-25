using AutoMapper;
using Models;
using PlatformService.DTOs;

namespace PlatformService.Profiles {
    public class PlatformsProfile : Profile {
        public PlatformsProfile() {
            CreateMap<PlatformModel, PlaformReadDto>();
            CreateMap<PlatformCreateDto, PlatformModel>();
            CreateMap<PlaformReadDto, PlatformPublishDto>();
        }
    }
}