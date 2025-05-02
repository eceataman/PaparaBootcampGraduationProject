using Microsoft.AspNetCore.Mvc;
using Papara.ExpenseTrackingSystem.API.DTOs;
using Papara.ExpenseTrackingSystem.API.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var expenses = await _expenseService.GetExpensesByUserIdAsync(userId);
        return Ok(expenses);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ExpenseCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        int userId = 2; // TODO: JWT token’dan alınacak (ClaimsPrincipal)
        await _expenseService.CreateExpenseAsync(dto, userId);
        return Ok(new { message = "Masraf başarıyla oluşturuldu." });
    }

    [HttpPut("approve/{id}")]
    public async Task<IActionResult> Approve(int id)
    {
        await _expenseService.ApproveExpenseAsync(id);
        return Ok(new { message = "Masraf talebi onaylandı." });
    }

    [HttpPut("reject/{id}")]
    public async Task<IActionResult> Reject(int id, [FromBody] string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            return BadRequest("Red nedeni girilmelidir.");

        await _expenseService.RejectExpenseAsync(id, reason);
        return Ok(new { message = "Masraf talebi reddedildi." });
    }
}
