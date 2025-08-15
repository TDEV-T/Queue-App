using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QueueService.Controllers;
using QueueService.Services.Interfaces;
using QueueService.DTOs;
using QueueService.Utility;
using System;
using System.Threading.Tasks;

namespace QueueServiceTests.UnitTests
{
    public class QueueControllerTests
    {
        private readonly Mock<IQueueService> _mockQueueService;
        private readonly Mock<ILogger<QueueController>> _mockLogger;
        private readonly QueueController _controller;

        public QueueControllerTests()
        {
            _mockQueueService = new Mock<IQueueService>();
            _mockLogger = new Mock<ILogger<QueueController>>();
            _controller = new QueueController(_mockQueueService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateTicket_WhenServiceSuccess_ReturnOkWithTicketData()
        {
            var expectedTicket = new TicketDto
            {
                TicketNumber = "A1",
                IssuedAt = DateTime.UtcNow
            };

            _mockQueueService.Setup(service => service.CreateNextTicketAsync())
                .ReturnsAsync(expectedTicket);
            var result = await _controller.CreateTicket();

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            okResult.Value.Should().BeOfType<ApiResponse<TicketDto>>();
            var apiResponse = okResult.Value as ApiResponse<TicketDto>;

            apiResponse.IsSuccess.Should().BeTrue();
            apiResponse.StatusCode.Should().Be(200);
            apiResponse.Data.Should().NotBeNull();
            apiResponse.Data.TicketNumber.Should().Be("A1");

        }


        [Fact]
        public async Task CreateTicket_WhenServiceThrowsException_ShouldReturnInternalServerError()
        {
        
            _mockQueueService.Setup(service => service.CreateNextTicketAsync())
                .ThrowsAsync(new Exception("Database connection failed"));

            var result = await _controller.CreateTicket();

          
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);

            objectResult.Value.Should().BeOfType<ApiResponse<object>>();
            var apiResponse = objectResult.Value as ApiResponse<object>;
            apiResponse.IsSuccess.Should().BeFalse();
            apiResponse.Message.Should().Be("An internal server error occurred.");
        }



        [Fact]
        public async Task GetCurrentTicket_WhenServiceSucceeds_ShouldReturnOkWithCurrentTicketData()
        {

            var expectedData = new CurrentTicketDto
            {
                CurrentTicketNumber = "A5",
            };


            _mockQueueService.Setup(service => service.GetCurrentTicket())
                .ReturnsAsync(expectedData);

 
            var result = await _controller.GetCurrentTicket();

  
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

     
            okResult.Value.Should().BeOfType<ApiResponse<CurrentTicketDto>>();
            var apiResponse = okResult.Value as ApiResponse<CurrentTicketDto>;

            apiResponse.IsSuccess.Should().BeTrue();
            apiResponse.StatusCode.Should().Be(200);
            apiResponse.Data.Should().NotBeNull();
            apiResponse.Data.CurrentTicketNumber.Should().Be("A5"); 
        }

        [Fact]
        public async Task ResetQueue_WhenServiceSucceeds_ShouldReturnOk()
        {
           
            _mockQueueService.Setup(service => service.ResetQueueAsync())
                .Returns(Task.CompletedTask);

            var result = await _controller.ResetQueue();

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var apiResponse = okResult.Value as ApiResponse<object>;

            apiResponse.IsSuccess.Should().BeTrue();
            apiResponse.Message.Should().Be("Queue has been reset successfully.");
        }
    }
}
