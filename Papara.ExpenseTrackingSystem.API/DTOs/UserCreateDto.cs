using System.ComponentModel.DataAnnotations;

public class UserCreateDto
{
    [Required]
    public string FullName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(6)]
    public string Password { get; set; }

    [Required, RegularExpression(@"^TR\d{24}$", ErrorMessage = "Geçerli bir IBAN giriniz.")]
    public string IBAN { get; set; }

    [Range(1, 2, ErrorMessage = "Rol değeri 1 (Admin) veya 2 (Personel) olmalıdır.")]
    public int Role { get; set; }
}
