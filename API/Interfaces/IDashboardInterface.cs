using API.Dtos;

namespace API.Interfaces
{
    public interface IDashboardInterface
    {
        Task<VetDashDto> GetDataForVet(string vetId);
        Task<OwnerDashDto> GetDataForOwner(string vetId);
    }
}
