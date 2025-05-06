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
            // VALIDATION: Geçerli kategori var mı?
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!categoryExists)
                throw new ArgumentException("Geçersiz kategori ID.");

            // Masraf nesnesi oluşturuluyor
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
                    FilePath = f.FileName
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

            // ✅ Hayali ödeme simülasyonu (loglama)
            Console.WriteLine($"[SIMULATED PAYMENT] Kullanıcı {expense.UserId} için {expense.Amount} TL ödendi.");
        }

        public async Task RejectExpenseAsync(int expenseId, string reason)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense is null) return;

            expense.Status = ExpenseStatus.Rejected;
            expense.RejectionReason = reason;

            await _context.SaveChangesAsync();
        }
        public async Task<List<ExpenseDto>> GetAllExpensesAsync()
        {
            return await _context.Expenses
                .Include(e => e.Category)
                .Include(e => e.User)
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
        public async Task<List<ExpenseDto>> FilterExpensesAsync(int userId, string? status, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Expenses
                .Where(e => e.UserId == userId)
                .Include(e => e.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<ExpenseStatus>(status, true, out var parsedStatus))
                query = query.Where(e => e.Status == parsedStatus);

            if (startDate.HasValue)
                query = query.Where(e => e.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(e => e.CreatedAt <= endDate.Value);

            return await query.Select(e => new ExpenseDto
            {
                Id = e.Id,
                CategoryName = e.Category.Name,
                Amount = e.Amount,
                PaymentTool = e.PaymentTool,
                Location = e.Location,
                CreatedAt = e.CreatedAt,
                Status = e.Status.ToString(),
                RejectionReason = e.RejectionReason
            }).ToListAsync();
        }


    }
}