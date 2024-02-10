using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Pets DTO
            CreateMap<Pet, PetDto>();
            CreateMap<UpdatePetDto, Pet>();

            // Medical History DTO
            CreateMap<MedicalHistory, CreateMedicalHistoryDto>(); 
            CreateMap<UpdateMedicalHistoryDto, MedicalHistory>(); 
            CreateMap<MedicalHistoryDto, CreateMedicalHistoryDto>(); 
        }
    }
}