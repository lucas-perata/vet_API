using API.Dtos.Spending;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ISpendingInterface
    {
        Task<bool> Complete();
        void AddSpending(Spending spending);
        void UpdateSpending(Spending spending);
        bool DeleteSpending(Spending spending);
        Task<Spending> GetSpending(int id);
        Task<PagedList<SpendingDto>> GetSpendingsForOwner(string ownerId, UserParams userParams);
        Task<PagedList<Spending>> GetSpendingsForOwnerMonthly(
            string ownerId,
            UserParams userParams
        );
        Task<decimal> GetTotalSpendingForOwner(string ownerId);
        Task<decimal> GetTotalSpendingForOwnerCurrentYear(string ownerId);
        Task<decimal> GetTotalSpendingForOwnerMonthly(string ownerId, int month, int year);
        Task<PagedList<SpendingDto>> GetSpendingsForCategory(
            string ownerId,
            SpendingCategory category,
            UserParams userParams
        );
        Task<PagedList<SpendingDto>> GetSpendingsForDateRange(
            string ownerId,
            DateTime startDate,
            DateTime endDate,
            UserParams userParams
        );
        Task<PagedList<SpendingDto>> GetSpendingsForPet(
            int petId,
            string ownerId,
            UserParams userParams
        );
        Task<List<MonthlySpendingSummaryDto>> GetSumSixMonthts(string ownerId);
        Task<List<CategorySumDto>> GetCategorySpendingForCurrentMonth(string ownerId);
    }
}
