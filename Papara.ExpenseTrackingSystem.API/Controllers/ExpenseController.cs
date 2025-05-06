using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.ExpenseTrackingSystem.API.DTOs;
using Papara.ExpenseTrackingSystem.API.Interfaces;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    private int GetUserIdFromToken() =>
        int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    private string GetUserRole() =>
        User.FindFirst(ClaimTypes.Role)?.Value!;

    [HttpGet("user")]
    public async Task<IActionResult> GetByUser()
    {
        var userId = GetUserIdFromToken();
        var expenses = await _expenseService.GetExpensesByUserIdAsync(userId);
        return Ok(expenses);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var expenses = await _expenseService.GetAllExpensesAsync();
        return Ok(expenses);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ExpenseCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        int userId = GetUserIdFromToken();
        await _expenseService.CreateExpenseAsync(dto, userId);
        return Ok(new { message = "Masraf başarıyla oluşturuldu." });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("approve/{id}")]
    public async Task<IActionResult> Approve(int id)
    {
        await _expenseService.ApproveExpenseAsync(id);
        return Ok(new { message = "Masraf talebi onaylandı." });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("reject/{id}")]
    public async Task<IActionResult> Reject(int id, [FromBody] string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            return BadRequest("Red nedeni girilmelidir.");

        await _expenseService.RejectExpenseAsync(id, reason);
        return Ok(new { message = "Masraf talebi reddedildi." });
    }
}
