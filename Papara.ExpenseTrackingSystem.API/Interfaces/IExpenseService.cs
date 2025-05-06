using Papara.ExpenseTrackingSystem.API.DTOs;

namespace Papara.ExpenseTrackingSystem.API.Interfaces
{
    public interface IExpenseService
    {
        Task<List<ExpenseDto>> GetExpensesByUserIdAsync(int userId);
        Task<ExpenseDto> GetExpenseByIdAsync(int id);
        Task CreateExpenseAsync(ExpenseCreateDto dto, int userId);
        Task ApproveExpenseAsync(int id);
        Task RejectExpenseAsync(int id, string reason);
        Task<List<ExpenseDto>> GetAllExpensesAsync();
        Task<List<ExpenseDto>> FilterExpensesAsync(int userId, string? status, DateTime? startDate, DateTime? endDate);


    }

}
