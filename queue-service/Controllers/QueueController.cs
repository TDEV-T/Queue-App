using Microsoft.AspNetCore.Mvc;
using QueueService.DTOs;
using QueueService.Services.Interfaces;
using QueueService.Utility;
namespace QueueService.Controllers;


[ApiController]
[Route("api/queue")] 
public class QueueController : ControllerBase
{
    private readonly IQueueService _queueService;
    private readonly ILogger<QueueController> _logger;

    public QueueController(IQueueService queueService, ILogger<QueueController> logger)
    {
        _queueService = queueService;
        _logger = logger;
    }

   
    [HttpPost("tickets")]
    [ProducesResponseType(typeof(ApiResponse<TicketDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse<object>), 500)]
    public async Task<IActionResult> CreateTicket()
    {
        try
        {
            var ticketDto = await _queueService.CreateNextTicketAsync();
            var response = ApiResponse<TicketDto>.Success(ticketDto, "Ticket created successfully.");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a ticket.");
            var errorResponse = ApiResponse<object>.Fail("An internal server error occurred.", 500);
            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost("tickets/current")]
    [ProducesResponseType(typeof(ApiResponse<TicketDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse<object>), 500)]

    public async Task<IActionResult> GetCurrentTicket()
    {
        try
        {
            var currentTicket = await _queueService.GetCurrentTicket();
            var response  = ApiResponse<CurrentTicketDto>.Success(currentTicket, "Current ticket retrieved successfully.");
            return Ok(response);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the current ticket.");
            var errorResponse = ApiResponse<object>.Fail("An internal server error occurred.", 500);
            return StatusCode(500, errorResponse);
        }
    }


    [HttpPost("reset")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    [ProducesResponseType(typeof(ApiResponse<object>), 500)]
    public async Task<IActionResult> ResetQueue()
    {
        try
        {
            await _queueService.ResetQueueAsync();
            var response = ApiResponse<object>.Success(null, "Queue has been reset successfully.");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while resetting the queue.");
            var errorResponse = ApiResponse<object>.Fail("An internal server error occurred.", 500);
            return StatusCode(500, errorResponse);
        }
    }
}