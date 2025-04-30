using Papara.ExpenseTrackingSystem.Domain.Entities;

public class Expense
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }

    public decimal Amount { get; set; }
    public string PaymentTool { get; set; } // örn: Kredi Kartı, Nakit
    public string Location { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;
    public string? RejectionReason { get; set; }

    public User User { get; set; }
    public Category Category { get; set; }
    public ICollection<ExpenseFile> Files { get; set; }
}
