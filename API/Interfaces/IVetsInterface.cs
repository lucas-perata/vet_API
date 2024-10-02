using API.Dtos;
using API.Entities.Identity;
using API.Helpers;

namespace API.Interfaces
{
    public interface IVetsInterface
    {
        Task<PagedList<VetDto>> GetVetList(UserParams userParams, AppUser user);
    }
}
