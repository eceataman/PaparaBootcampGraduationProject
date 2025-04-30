
using Papara.ExpenseTrackingSystem.Domain.Entities;
using System.Data;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string IBAN { get; set; }
    public Role Role { get; set; }

    public ICollection<Expense> Expenses { get; set; }
}

