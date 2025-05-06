namespace Papara.ExpenseTrackingSystem.API.DTOs
{
    public class AuthResultDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
    }

}
