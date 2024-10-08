using API.Dtos;
using API.Dtos.Adoption;
using API.Dtos.Appointment;
using API.Dtos.Message;
using API.Dtos.Photo;
using API.Dtos.Review;
using API.Dtos.Service;
using API.Dtos.Spending;
using API.Entities;
using API.Entities.Identity;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Pets
            CreateMap<Pet, PetDto>()
                .ForMember(dest => dest.PetPhoto, opt => opt.MapFrom(src => src.PetPhotos));
            CreateMap<UpdatePetDto, Pet>();

            // Medical Histories
            CreateMap<MedicalHistory, CreateMedicalHistoryDto>();
            CreateMap<UpdateMedicalHistoryDto, MedicalHistory>();
            CreateMap<MedicalHistoryDto, CreateMedicalHistoryDto>();
            CreateMap<MedicalHistory, MedicalHistoryDto>();

            // Adoptions
            CreateMap<Adoption, AdoptionDto>()
                .ForMember(
                    dest => dest.AdoptionPhoto,
                    opt => opt.MapFrom(src => src.AdoptionPhotos)
                );
            CreateMap<Adoption, CreateAdoptionDto>();
            CreateMap<Adoption, UpdateAdoptionDto>();
            CreateMap<UpdateAdoptionDto, Adoption>();

            // Services
            CreateMap<VetService, AddVetServiceDto>();

            // Appointments
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<Appointment, RequestAppointmentDto>();

            // Reviews
            CreateMap<Review, ReviewDto>();
            CreateMap<Review, CreateReviewDto>();
            CreateMap<Review, UpdateReviewDto>();
            CreateMap<UpdateReviewDto, Review>();

            // Messages
            CreateMap<Message, MessageDto>();
            CreateMap<Message, CreateMessageDto>();

            // Spendings
            CreateMap<Spending, SpendingDto>();
            CreateMap<Spending, CreateSpendingDto>();
            CreateMap<Spending, UpdateSpendingDto>();
            CreateMap<UpdateSpendingDto, Spending>();

            // Photos
            CreateMap<Photo, PhotoDto>();
            CreateMap<PetPhoto, PetPhotoDto>();
            CreateMap<AdoptionPhoto, AdoptionPhotoDto>();

            // Vaccines
            CreateMap<Vaccine, VaccineDto>();
            CreateMap<Vaccine, CreateVaccineDto>();
            CreateMap<Vaccine, UpdateVaccineDto>();

            // Expenses
            CreateMap<ExpensesVet, ExpensesVetDto>();
            CreateMap<ExpensesVetDto, ExpensesVet>();
            CreateMap<CreateExpenseDto, ExpensesVet>();
            CreateMap<UpdateExpenseDto, ExpensesVet>();
            CreateMap<ExpensesVet, CreateExpenseDto>();

            // Followers
            CreateMap<Follower, FollowerDto>();
            CreateMap<Follower, FollowDto>();
            CreateMap<FollowerDto, Follower>();
            CreateMap<FollowDto, Follower>();
        }
    }
}
