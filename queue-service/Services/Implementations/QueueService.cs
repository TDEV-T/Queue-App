using QueueService.DTOs;
using QueueService.Models;
using QueueService.Repository.Interfaces;
using QueueService.Services.Interfaces;
using System.Collections.Concurrent;

namespace QueueService.Services.Implementations;

public class QueueService : IQueueService
{
    private readonly IQueueStateRepository _queueStateRepo;
    private readonly ITicketRepository _ticketRepo;


    public QueueService(IQueueStateRepository queueStateRepo, ITicketRepository ticketRepo)
    {
        _queueStateRepo = queueStateRepo;
        _ticketRepo = ticketRepo;
    }

    public async Task<TicketDto> CreateNextTicketAsync()
    {
        while(true)
        {
            try
            {
                var state = await _queueStateRepo.GetCurrentStateAsync();

                if (state.CurrentNumber >= 9)
                {
                    state.CurrentNumber = 0;
                    state.CurrentPrefix = (state.CurrentPrefix == 'Z') ? 'A' : (char)(state.CurrentPrefix + 1);
                }
                else
                {
                    state.CurrentNumber++;
                }

                var newTicket = new Ticket
                {
                    TicketNumber = $"{state.CurrentPrefix}{state.CurrentNumber}",
                    IssuedAt = DateTime.UtcNow,
                    Status = "Waiting"
                };

                await _ticketRepo.AddAsync(newTicket);
                await _queueStateRepo.UpdateStateAsync(state);

                return new TicketDto
                {
                    TicketNumber = newTicket.TicketNumber,
                    IssuedAt = newTicket.IssuedAt
                };
            }
            catch (Exception ex) {
                await Task.Delay(50);
            }
        }
    }

    public async Task<CurrentTicketDto> GetCurrentTicket()
    {
        while (true)
        {
            try
            {
                var state = await _queueStateRepo.GetCurrentStateAsync();

                var currentTicketNumber = (state.CurrentNumber == -1) ? "0" : $"{state.CurrentPrefix}{state.CurrentNumber}";



                return new CurrentTicketDto
                {
                    CurrentTicketNumber = currentTicketNumber
                };
            }
            catch (Exception ex)
            {
                await Task.Delay(50);
            }
        }
    }


    public async Task ResetQueueAsync()
    {
        var state = await _queueStateRepo.GetCurrentStateAsync() ;

        state.CurrentPrefix = 'A';
        state.CurrentNumber = -1;

        await _queueStateRepo.UpdateStateAsync(state);
        await _ticketRepo.CancelAllWaitingTicketsAsync();
    }
}
