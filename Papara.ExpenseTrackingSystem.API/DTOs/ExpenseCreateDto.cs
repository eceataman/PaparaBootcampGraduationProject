using System.ComponentModel.DataAnnotations;

public class ExpenseCreateDto
{
    [Required]
    public int CategoryId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Tutar 0'dan büyük olmalıdır.")]
    public decimal Amount { get; set; }

    [Required]
    public string PaymentTool { get; set; }

    [Required]
    public string Location { get; set; }

    public List<IFormFile>? Files { get; set; }
}
