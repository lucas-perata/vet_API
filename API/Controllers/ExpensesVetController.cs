using System.Security.Claims;
using API.Dtos;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Helpers;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpensesVetController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ExpenseVetRepository _expenseVetRepository;

        public ExpensesVetController(
            IMapper mapper,
            UserManager<AppUser> userManager,
            ExpenseVetRepository expenseVetRepository
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _expenseVetRepository = expenseVetRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<ExpensesVetDto>>> GetAllExpenses(
            [FromQuery] UserParams userParams
        )
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            AppUser user = await _userManager.FindByEmailAsync(email);

            var expenses = await _expenseVetRepository.GetExpensesForVet(user.Id, userParams);

            Response.AddPaginationHeader(
                new PaginationHeader(
                    expenses.CurrentPage,
                    expenses.PageSize,
                    expenses.TotalCount,
                    expenses.TotalPages
                )
            );

            return Ok(expenses);
        }

        [HttpGet("/monthly-expenses")]
        public async Task<ActionResult<PagedList<ExpensesVetDto>>> GetMonthlyVetExpenses(
            [FromQuery] UserParams userParams
        )
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            AppUser user = await _userManager.FindByEmailAsync(email);

            var expenses = await _expenseVetRepository.GetExpensesVetMonthly(user.Id, userParams);

            if (expenses is null)
                return NotFound();

            Response.AddPaginationHeader(
                new PaginationHeader(
                    expenses.CurrentPage,
                    expenses.PageSize,
                    expenses.TotalCount,
                    expenses.TotalPages
                )
            );

            return Ok(expenses);
        }

        [HttpPost]
        public async Task<ActionResult<ExpensesVetDto>> CreateExpense(
            CreateExpenseDto createExpenseDto
        )
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return Unauthorized();

            var expense = new ExpensesVet
            {
                VetId = user.Id,
                Description = createExpenseDto.Description,
                Amount = createExpenseDto.Amount,
                Category = createExpenseDto.Category,
                Date = createExpenseDto.Date,
            };

            _expenseVetRepository.AddExpense(expense);

            if (await _expenseVetRepository.Complete())
                return Ok(_mapper.Map<ExpensesVetDto>(expense));

            return BadRequest("Failed to create");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExpense(int id, UpdateExpenseDto updateExpenseDto)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            AppUser user = await _userManager.FindByEmailAsync(email);

            ExpensesVet expense = await _expenseVetRepository.GetExpense(user.Id, id);

            if (expense is null)
                return NotFound();

            _mapper.Map(updateExpenseDto, expense);

            _expenseVetRepository.UpdateExpense(expense);

            if (await _expenseVetRepository.Complete())
                return Ok();

            return BadRequest("Failed to update");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense(int id)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            AppUser user = await _userManager.FindByEmailAsync(email);

            ExpensesVet expense = await _expenseVetRepository.GetExpense(user.Id, id);

            if (expense is null)
                return NotFound();

            var result = _expenseVetRepository.DeleteExpense(expense);

            if (!result)
                return BadRequest("There was a problem deleting the expense");

            return NoContent();
            ;
        }
    }
}
