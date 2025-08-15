using QueueService.Models;

namespace QueueService.Repository.Interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
        Task CancelAllWaitingTicketsAsync();
    }
}
