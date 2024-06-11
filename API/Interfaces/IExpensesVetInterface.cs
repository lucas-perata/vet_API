using API.Dtos;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IExpenseVetInterface
    {
        Task<bool> Complete();
        void AddExpense(ExpensesVet expenseVet);
        void UpdateExpense(ExpensesVet expenseVet);
        bool DeleteExpense(ExpensesVet expenseVet);
        Task<PagedList<ExpensesVet>> GetExpensesVetMonthly(string vetId, UserParams userParams);
        Task<ExpensesVet> GetExpense(string vetId, int id);
        Task<PagedList<ExpensesVetDto>> GetExpensesForVet(string vetId, UserParams userParams);
        //TODO: Add the optimized calls
    }
}
