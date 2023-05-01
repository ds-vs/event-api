using Event.Domain.Dto.Event;
using Event.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Event.API.Controllers
{
    /// <summary> Контроллер, работающий с EventService. </summary>
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
        [HttpGet, Route("event/all")]
        [ApiExplorerSettings(GroupName = "Guests")]
        public IActionResult GetEvents()
        {
            var response = _eventService.Get();

            if(response.Status == HttpStatusCode.OK)
                return Ok(response.Data);

            return NotFound();
        }

        /// <summary> Получить информацию о конкретном мероприятии. </summary>
        /// <remarks> Пример запроса: GET api/event/id. </remarks> 
        /// <param name="id"> Идентификатор мероприятия. </param>
        /// <returns> Мероприятие. </returns>
        [HttpGet, Route("event/id")]
        [ApiExplorerSettings(GroupName = "Guests")]
        public IActionResult GetEvent(Guid id)
        {
            var response = _eventService.Get(id);

            if (response.Status == HttpStatusCode.OK)
                return Ok(response.Data);

            return NotFound(response.Description);
        }

        /// <summary> Получить информацию о своих созданных мероприятиях. </summary>
        /// <remarks> Пример запроса: GET api/event/created. </remarks> 
        /// <returns> Мероприятия. </returns>
        [HttpGet, Route("event/created"), Authorize(Roles = "Organizer")]
        [ApiExplorerSettings(GroupName = "Organizers")]
        public async Task<IActionResult> GetCreatedEvents()
        {
            var login = User!.Identity!.Name;

            var response = await _eventService.Get(login!);

            if (response.Status == HttpStatusCode.OK)
                return Ok(response.Data);

            return NotFound();
        }

        /// <summary> Удалить мероприятие. </summary>
        /// <remarks> Пример запроса: DELETE api/event/delete/id. </remarks> 
        /// <param name="id"> Идентификатор мероприятия. </param>
        /// <returns> Сообщение о удалении мероприятия. </returns>
        [HttpDelete, Route("event/delete/id"), Authorize(Roles = "Organizer")]
        [ApiExplorerSettings(GroupName = "Organizers")]
        public async Task<IActionResult> DeleteEvents(Guid id)
        {
            var login = User!.Identity!.Name;

            var response = await _eventService.DeleteEventAsync(login!, id);

            if (response.Status == HttpStatusCode.OK)
                return Ok(response.Description);

            return NotFound();
        }

        /// <summary> Создать новое мероприятие. </summary>
        /// <remarks> Пример запроса: POST api/event/create. </remarks> 
        /// <param name="request"> Модель данных для создания мероприятия. </param>
        /// <returns> Сообщение о создании мероприятия. </returns>
        [HttpPost, Authorize(Roles = "Organizer"), Route("event/create")]
        [ApiExplorerSettings(GroupName = "Organizers")]
        public async Task<IActionResult> CreateEventAsync(CreateEventDto request)
        {
            var login = User!.Identity!.Name;

            var response = await _eventService.CreateEventAsync(login!, request);

            if (response.Status == HttpStatusCode.OK)
                return Ok("Event created successfully.");

            return BadRequest("Failed to create event.");
        }

        /// <summary> Изменить существующее мероприятие. </summary>
        /// <remarks> Пример запроса: PUT api/event/update. </remarks>
        /// <param name="id"> Идентификатор события. </param> 
        /// <param name="request"> Модель данных для создания мероприятия. </param>
        /// <returns> Сообщение о создании мероприятия. </returns>
        [HttpPut, Authorize(Roles = "Organizer"), Route("event/update/id")]
        [ApiExplorerSettings(GroupName = "Organizers")]
        public async Task<IActionResult> UpdateEventAsync(Guid id, CreateEventDto request)
        {
            var login = User!.Identity!.Name;

            var response = await _eventService.UpdateEventAsync(login!, id, request);

            if (response.Status == HttpStatusCode.OK)
                return Ok(response.Description);

            return BadRequest("Failed to update event.");
        }
    }
}
