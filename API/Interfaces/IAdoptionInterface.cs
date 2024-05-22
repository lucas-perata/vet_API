using API.Dtos.Adoption;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IAdoptionInterface
    {
        Task<PagedList<AdoptionDto>> GetAdoptions(UserParams userParams);
        Task<Adoption> GetAdoption(int id);
        void CreateAdoptionAsync(Adoption adoption);
        Task<bool> Complete();
        void UpdateAdoption(Adoption adoption);
        bool DeleteAdoption(Adoption adoption);
        Task<PagedList<AdoptionDto>> SearchAdoptions(
            UserParams userParams,
            string gender,
            string area,
            string province
        );
    }
}
