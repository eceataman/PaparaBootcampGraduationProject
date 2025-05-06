using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Papara.ExpenseTrackingSystem.API.DTOs;

public class ReportService : IReportService
{
    private readonly IConfiguration _configuration;

    public ReportService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<ExpenseSummaryDto>> GetExpenseSummaryAsync()
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var sql = "SELECT * FROM vw_UserExpenseSummary";
        var result = await connection.QueryAsync<ExpenseSummaryDto>(sql);
        return result.ToList();
    }
    public async Task<List<UserExpenseHistoryDto>> GetUserExpenseHistoryAsync(int userId)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var sql = "SELECT * FROM vw_UserExpenseHistory WHERE UserId = @UserId";
        var result = await connection.QueryAsync<UserExpenseHistoryDto>(sql, new { UserId = userId });
        return result.ToList();
    }
    public async Task<TotalExpenseDto> GetTotalExpensesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        var sql = @"
        SELECT SUM(Amount) AS TotalAmount
        FROM Expenses
        WHERE Status = 1 -- Approved
        AND CreatedAt BETWEEN @StartDate AND @EndDate
    ";

        var result = await connection.QueryFirstOrDefaultAsync<TotalExpenseDto>(sql, new
        {
            StartDate = startDate,
            EndDate = endDate
        });

        return result ?? new TotalExpenseDto { TotalAmount = 0 };
    }
    public async Task<List<MonthlyUserExpenseDto>> GetMonthlyUserExpensesAsync()
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var sql = "SELECT * FROM vw_MonthlyUserExpense";
        var result = await connection.QueryAsync<MonthlyUserExpenseDto>(sql);
        return result.ToList();
    }



}
