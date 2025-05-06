namespace Papara.ExpenseTrackingSystem.API.DTOs
{
    public class UserExpenseHistoryDto
    {
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
        public string PaymentTool { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }

}
