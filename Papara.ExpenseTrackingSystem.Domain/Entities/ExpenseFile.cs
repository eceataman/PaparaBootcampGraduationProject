
using Papara.ExpenseTrackingSystem.Domain.Entities;
public class ExpenseFile
{
    public int Id { get; set; }
    public int ExpenseId { get; set; }
    public string FilePath { get; set; }

    public Expense Expense { get; set; }
}
