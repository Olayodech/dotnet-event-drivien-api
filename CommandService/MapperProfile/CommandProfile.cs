using AutoMapper;
using CommandService.DTO;
using CommandService.Models;

namespace MapperProfile {
    public class PlatformProfile : Profile {
        public PlatformProfile() {
            CreateMap<PlatformModels, PlatformReadDto>();
            CreateMap<CommandModel, CommandReadDto>();
            CreateMap<CommandCreateDto, CommandModel>();
        }
    }
}