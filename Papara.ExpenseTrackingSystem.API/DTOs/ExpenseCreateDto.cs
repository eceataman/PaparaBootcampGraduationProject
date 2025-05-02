namespace Papara.ExpenseTrackingSystem.API.DTOs
{
    public class ExpenseCreateDto
    {
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentTool { get; set; }
        public string Location { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}

