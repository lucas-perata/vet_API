using API.Data;
using API.Dtos;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ExpenseVetRepository : IExpenseVetInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ExpenseVetRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddExpense(ExpensesVet expenseVet)
        {
            _context.ExpensesVet.Add(expenseVet);
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool DeleteExpense(ExpensesVet expenseVet)
        {
            _context.Remove(expenseVet);
            return _context.SaveChanges() > 0;
        }

        public async Task<ExpensesVet> GetExpense(string vetId, int id)
        {
            var expenses = _context
                .ExpensesVet.Where(e => e.Id == id && e.VetId == vetId)
                .FirstOrDefault();

            return expenses;
        }

        public async Task<PagedList<ExpensesVetDto>> GetExpensesForVet(
            string vetId,
            UserParams userParams
        )
        {
            var query = _context
                .ExpensesVet.Where(e => e.VetId == vetId)
                .ProjectTo<ExpensesVetDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<ExpensesVetDto>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }

        public async Task<PagedList<ExpensesVet>> GetExpensesVetMonthly(
            string vetId,
            UserParams userParams
        )
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            var query = _context
                .ExpensesVet.Where(e =>
                    e.VetId == vetId && e.Date.Month == currentMonth && e.Date.Year == currentYear
                )
                .OrderBy(s => s.Date)
                .AsNoTracking();

            return await PagedList<ExpensesVet>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }

        public void UpdateExpense(ExpensesVet expenseVet)
        {
            _context.Entry(expenseVet).State = EntityState.Modified;
        }
    }
}
