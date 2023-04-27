using Event.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Event.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary> 
        /// <remarks> Пример запроса: GET api/event/all. </remarks> 
        /// </summary>
        /// <returns> Все актуальные мероприятия. </returns>
        [HttpGet]
        [Route("event/all")]
        public IActionResult GetEvents()
        {
            var response = _eventService.Get();

            if(response.Status == HttpStatusCode.OK)
                return Ok(response.Data);

            return NotFound();
        }
    }
}
