using Event.API.Controllers;
using Event.Domain.Dto.Event;
using Event.Domain.Entities;
using Event.Domain.Enums;
using Event.Domain.Repositories;
using Event.Domain.Repositories.Interfaces;
using Event.Service;
using Microsoft.AspNetCore.Mvc;

namespace Event.Test.ControllerTests
{
    public class EventControllerTests
    {
        [Fact]
        public void GetEvents_ReturnsAllCurrentEvents()
        {
            // Setup
            var mockAccount = new Mock<IAccountRepository>();
            var mockEvent = new Mock<IEventRepository>();

            mockEvent.Setup(m => m.Get()).Returns((new EventEntity[]
            {
                new EventEntity
                {
                    Title = "Title 1",
                    Status = StatusType.Completed
                },
                new EventEntity
                {
                    Title = "Title 2",
                    Responses = 0,
                    Status = StatusType.Actual
                }
            }).AsQueryable());

            var service = new EventService(mockEvent.Object, mockAccount.Object);
            var controller = new EventController(service);

            // Act
            var values = ((controller.GetEvents() as OkObjectResult)!.Value as IEnumerable<GetEventDto>)!.ToList();

            // Assert
            Assert.Equal("Title 2", values.First().Title);
            Assert.NotEqual(2, values.Count);
        }
    }
}
