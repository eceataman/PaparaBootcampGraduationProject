namespace Papara.ExpenseTrackingSystem.API.DTOs
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
        public string PaymentTool { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string? RejectionReason { get; set; }
    }


}
