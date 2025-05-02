using Microsoft.EntityFrameworkCore;
using Papara.ExpenseTrackingSystem.API.DTOs;
using Papara.ExpenseTrackingSystem.API.Interfaces;
using Papara.ExpenseTrackingSystem.Domain.Entities;
using Papara.ExpenseTrackingSystem.Domain;
using Persistence;

namespace Papara.ExpenseTrackingSystem.API.Services
{

    public class ExpenseService : IExpenseService
    {
        private readonly PaparaDbContext _context;

        public ExpenseService(PaparaDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpenseDto>> GetExpensesByUserIdAsync(int userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .Include(e => e.Category)
                .Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    CategoryName = e.Category.Name,
                    Amount = e.Amount,
                    PaymentTool = e.PaymentTool,
                    Location = e.Location,
                    CreatedAt = e.CreatedAt,
                    Status = e.Status.ToString(),
                    RejectionReason = e.RejectionReason
                })
                .ToListAsync();
        }

        public async Task<ExpenseDto?> GetExpenseByIdAsync(int expenseId)
        {
            var expense = await _context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == expenseId);

            if (expense == null) return null;

            return new ExpenseDto
            {
                Id = expense.Id,
                CategoryName = expense.Category.Name,
                Amount = expense.Amount,
                PaymentTool = expense.PaymentTool,
                Location = expense.Location,
                CreatedAt = expense.CreatedAt,
                Status = expense.Status.ToString(),
                RejectionReason = expense.RejectionReason
            };
        }

        public async Task CreateExpenseAsync(ExpenseCreateDto dto, int userId)
        {
            var newExpense = new Expense
            {
                UserId = userId,
                CategoryId = dto.CategoryId,
                Amount = dto.Amount,
                PaymentTool = dto.PaymentTool,
                Location = dto.Location,
                CreatedAt = DateTime.UtcNow,
                Status = ExpenseStatus.Pending,
                Files = dto.Files?.Select(f => new ExpenseFile
                {
                    FilePath = f.FileName // TODO: Gerçek dosya yolu eklenmeli
                }).ToList()
            };

            await _context.Expenses.AddAsync(newExpense);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveExpenseAsync(int expenseId)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense is null) return;

            expense.Status = ExpenseStatus.Approved;
            await _context.SaveChangesAsync();

            // TODO: Sanal ödeme simülasyonu entegre edilebilir
        }

        public async Task RejectExpenseAsync(int expenseId, string reason)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense is null) return;

            expense.Status = ExpenseStatus.Rejected;
            expense.RejectionReason = reason;

            await _context.SaveChangesAsync();
        }
    }
}