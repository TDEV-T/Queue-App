    namespace QueueService.DTOs
{
    public class TicketDto
    {
        public required string TicketNumber { get; set; }
        public DateTime IssuedAt { get; set; }
    }
}
