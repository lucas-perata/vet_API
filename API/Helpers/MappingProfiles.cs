using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pet, PetDto>();
            CreateMap<UpdatePetDto, Pet>();
        }
    }
}