using Microsoft.EntityFrameworkCore;
using QueueService.Models;
using QueueService.Repository.Interfaces;

namespace QueueService.Repository.Implementations
{
    public class QueueStateRepository : IQueueStateRepository
    {
        private readonly QueueDbContext _dbContext;
        public QueueStateRepository(QueueDbContext dbContext) { _dbContext = dbContext; }

        public Task<QueueState> GetCurrentStateAsync() {
            return _dbContext.QueueStates.SingleAsync(q => q.Id == 1);
        }

        public Task UpdateStateAsync(QueueState state)
        {
            _dbContext.QueueStates.Update(state);
            return _dbContext.SaveChangesAsync();
        }

     
    }
}
