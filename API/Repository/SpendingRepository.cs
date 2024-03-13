using API.Data;
using API.Dtos.Spending;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class SpendingRepository : ISpendingInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SpendingRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddSpending(Spending spending)
        {
            _context.Spendings.Add(spending);
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool DeleteSpending(Spending spending)
        {
            _context.Remove(spending);
            return _context.SaveChanges() > 0;
        }

        public async Task<Spending> GetSpending(int id)
        {
            return await _context.Spendings.FindAsync(id);
        }

        public async Task<PagedList<SpendingDto>> GetSpendingsForCategory(
            string ownerId,
            SpendingCategory category,
            UserParams userParams
        )
        {
            var query = _context
                .Spendings.Where(s => s.OwnerId == ownerId && s.Category == category)
                .ProjectTo<SpendingDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<SpendingDto>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }

        public async Task<PagedList<SpendingDto>> GetSpendingsForPet(
            int petId,
            string ownerId,
            UserParams userParams
        )
        {
            var query = _context
                .Spendings.Where(s => s.PetId == petId && s.OwnerId == ownerId)
                .ProjectTo<SpendingDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<SpendingDto>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }

        public async Task<PagedList<SpendingDto>> GetSpendingsForDateRange(
            string ownerId,
            DateTime startDate,
            DateTime endDate,
            UserParams userParams
        )
        {
            var query = _context
                .Spendings.Where(s =>
                    s.OwnerId == ownerId && s.Date >= startDate && s.Date <= endDate
                )
                .ProjectTo<SpendingDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<SpendingDto>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }

        public async Task<PagedList<SpendingDto>> GetSpendingsForOwner(
            string ownerId,
            UserParams userParams
        )
        {
            var query = _context
                .Spendings.Where(s => s.OwnerId == ownerId)
                .ProjectTo<SpendingDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<SpendingDto>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }

        public async Task<PagedList<Spending>> GetSpendingsForOwnerMonthly(
            string ownerId,
            UserParams userParams
        )
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var query = _context
                .Spendings.Where(s =>
                    s.OwnerId == ownerId
                    && s.Date.Month == currentMonth
                    && s.Date.Year == currentYear
                )
                .OrderBy(s => s.Date)
                .AsNoTracking();

            return await PagedList<Spending>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }

        public async Task<List<MonthlySpendingSummaryDto>> GetSumSixMonthts(string ownerId)
        {
            var currentDate = DateTime.UtcNow;

            var startDate = currentDate.AddMonths(-11);
            startDate = startDate.AddDays(1 - startDate.Day);
            var spendingData = await _context
                .Spendings.Where(s => s.Date >= startDate && s.Date < currentDate)
                .GroupBy(s => new { Month = s.Date.Month }) // Group only by month
                .Select(g => new MonthlySpendingSummaryDto // Use the defined DTO class
                {
                    Month = g.Key.Month,
                    TotalSpending = g.Sum(s => s.Amount)
                })
                .ToListAsync();

            return (spendingData);
        }

        public async Task<decimal> GetTotalSpendingForOwner(string ownerId)
        {
            return await _context
                .Spendings.Where(s => s.OwnerId == ownerId)
                .SumAsync(s => s.Amount);
        }

        public async Task<decimal> GetTotalSpendingForOwnerCurrentYear(string ownerId)
        {
            var currentYear = DateTime.Now.Year;

            return await _context
                .Spendings.Where(s => s.OwnerId == ownerId && s.Date.Year == currentYear)
                .SumAsync(s => s.Amount);
        }

        public async Task<decimal> GetTotalSpendingForOwnerMonthly(
            string ownerId,
            int month,
            int year
        )
        {
            return await _context
                .Spendings.Where(s =>
                    s.OwnerId == ownerId && s.Date.Month == month && s.Date.Year == year
                )
                .SumAsync(s => s.Amount);
        }

        public void UpdateSpending(Spending spending)
        {
            _context.Entry(spending).State = EntityState.Modified;
        }

        public async Task<List<CategorySumDto>> GetCategorySpendingForCurrentMonth(string ownerId)
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var spendingData = await _context
                .Spendings.Where(s => s.Date.Year == currentYear && s.Date.Month == currentMonth)
                .GroupBy(s => s.Category)
                .Select(g => new CategorySumDto
                {
                    CategoryName = g.Key.ToString(),
                    Total = g.Sum(s => s.Amount)
                })
                .ToListAsync();

            return spendingData;
        }
    }
}
