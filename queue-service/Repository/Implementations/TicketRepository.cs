using Microsoft.EntityFrameworkCore;
using QueueService.Models;
using QueueService.Repository.Interfaces;

namespace QueueService.Repository.Implementations
{
    public class TicketRepository: ITicketRepository
    {
        private readonly QueueDbContext _dbContext;
        public TicketRepository(QueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddAsync(Ticket ticket)
        {
            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();
        }
        public async Task CancelAllWaitingTicketsAsync()
        {
            await _dbContext.Tickets
                .Where(t => t.Status == "Waiting")
                .ExecuteUpdateAsync(setter => setter.SetProperty(t => t.Status, "Cancelled"));
        }

    }
}
