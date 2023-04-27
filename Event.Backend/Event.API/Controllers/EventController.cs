using Event.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Event.API.Controllers
{
    /// <summary>
    /// Контроллер, работающий с EventService.
    /// </summary>
    [Route("api/")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary> Получить информацию о всех актуальных мероприятиях. </summary>
        /// <remarks> Пример запроса: GET api/event/all. </remarks> 
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
