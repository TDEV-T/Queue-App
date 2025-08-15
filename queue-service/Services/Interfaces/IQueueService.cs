using QueueService.DTOs;

namespace QueueService.Services.Interfaces;

public interface IQueueService
{
    Task<TicketDto> CreateNextTicketAsync();
    Task<CurrentTicketDto> GetCurrentTicket();
    Task ResetQueueAsync();
}
