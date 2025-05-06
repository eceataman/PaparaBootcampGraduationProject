namespace Papara.ExpenseTrackingSystem.API.DTOs;

public class ExpenseSummaryDto
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Status { get; set; }
    public int TotalExpenses { get; set; }
    public decimal TotalAmount { get; set; }
}
