using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var result = await _reportService.GetExpenseSummaryAsync();
        return Ok(result);
    }
    [HttpGet("user-history")]
    public async Task<IActionResult> GetUserHistory()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _reportService.GetUserExpenseHistoryAsync(userId);
        return Ok(result);
    }
    [HttpGet("total-expenses")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetTotalExpenses([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _reportService.GetTotalExpensesByDateRangeAsync(startDate, endDate);
        return Ok(result);
    }
    [HttpGet("monthly-user-expenses")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetMonthlyUserExpenses()
    {
        var data = await _reportService.GetMonthlyUserExpensesAsync();
        return Ok(data);
    }



}
