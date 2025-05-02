namespace Papara.ExpenseTrackingSystem.API.DTOs
{
    public class UserCreateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IBAN { get; set; }
        public int Role { get; set; } // 1 = Admin, 2 = Personel
    }

}
