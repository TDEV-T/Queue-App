using QueueService.Models;

namespace QueueService.Repository.Interfaces
{
    public interface IQueueStateRepository
    {
        Task<QueueState> GetCurrentStateAsync();
        Task UpdateStateAsync(QueueState queueState);
    }
}
