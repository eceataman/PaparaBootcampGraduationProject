﻿namespace Papara.ExpenseTrackingSystem.API.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string IBAN { get; set; } // ✅ Eksik olan alan
    }

}
