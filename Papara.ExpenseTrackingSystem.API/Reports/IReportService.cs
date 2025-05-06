using Papara.ExpenseTrackingSystem.API.DTOs;

public interface IReportService
{
    Task<List<ExpenseSummaryDto>> GetExpenseSummaryAsync();
    Task<List<UserExpenseHistoryDto>> GetUserExpenseHistoryAsync(int userId);
    Task<TotalExpenseDto> GetTotalExpensesByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<List<MonthlyUserExpenseDto>> GetMonthlyUserExpensesAsync();



}
