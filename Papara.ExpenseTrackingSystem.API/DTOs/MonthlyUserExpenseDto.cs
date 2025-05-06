namespace Papara.ExpenseTrackingSystem.API.DTOs
{
    public class MonthlyUserExpenseDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
