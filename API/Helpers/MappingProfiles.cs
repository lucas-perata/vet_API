using API.Dtos;
using API.Dtos.Adoption;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Pets 
            CreateMap<Pet, PetDto>();
            CreateMap<UpdatePetDto, Pet>();

            // Medical Histories 
            CreateMap<MedicalHistory, CreateMedicalHistoryDto>(); 
            CreateMap<UpdateMedicalHistoryDto, MedicalHistory>(); 
            CreateMap<MedicalHistoryDto, CreateMedicalHistoryDto>(); 
            CreateMap<MedicalHistory, MedicalHistoryDto>();

            // Adoptions 
            CreateMap<Adoption, AdoptionDto>(); 
            CreateMap<Adoption, CreateAdoptionDto>(); 
            CreateMap<Adoption, UpdateAdoptionDto>();
            CreateMap<UpdateAdoptionDto, Adoption>(); 
        }
    }
}