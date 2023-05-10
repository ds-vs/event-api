using Event.Domain.Dto.Event;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary> Получить информацию о всех актуальных мероприятиях. </summary>
        /// <remarks> GET api/event/all </remarks> 
        /// <returns> Актуальные мероприятия. </returns>
        [HttpGet, Route("event/all")]
        [ApiExplorerSettings(GroupName = "Guests")]
        public IActionResult GetEvents()
        {
            var response = _eventService.Get();

            if(response.Status == HttpStatusCode.OK)
                return Ok(response.Data);

            return NotFound();
        }

        /// <summary> Подписаться на событие. </summary>
        /// <remarks> POST api/event/subscribe/id </remarks> 
        /// <param name="id"> Идентификатор мероприятия. </param>
        /// <returns> Пользователь подписан. </returns>
        [HttpPost, Route("event/subscribe/id"), Authorize(Roles = "Member")]
        [ApiExplorerSettings(GroupName = "Members")]
        public async Task<IActionResult> SubscribeEvent(Guid id)
        {
            var login = User!.Identity!.Name;
            var response = await _eventService.EventSubscribeAsync(login!, id);

            if(response.Status == HttpStatusCode.OK)
            {
                return Ok(response.Description);
            }
            return BadRequest();
        }

        /// <summary> Отписаться от события. </summary>
        /// <remarks> DELETE api/event/unsubscribe/id </remarks> 
        /// <param name="id"> Идентификатор мероприятия. </param>
        /// <returns> Подписка отменена. </returns>
        [HttpDelete, Route("event/unsubscribe/id"), Authorize(Roles = "Member")]
        [ApiExplorerSettings(GroupName = "Members")]
        public async Task<IActionResult> UnsubscribeEvent(Guid id)
        {
            var login = User!.Identity!.Name;
            var response = await _eventService.EventUnsubscribeAsync(login!, id);

            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response.Description);
            }
            return BadRequest();
        }

        /// <summary> Получить информацию о подписках. </summary>
        /// <remarks> GET api/event/subscriptions </remarks> 
        /// <returns> Мереприятия в подписках. </returns>
        [HttpGet, Route("event/subscriptions"), Authorize(Roles = "Member")]
        [ApiExplorerSettings(GroupName = "Members")]
        public async Task<IActionResult> GetSubscriptions()
        {
            var login = User!.Identity!.Name;
            var response = await _eventService.GetSubscriptions(login!);

            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return BadRequest();
        }

        /// <summary> Получить информацию о мероприятии. </summary>
        /// <remarks> GET api/event/id </remarks> 
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

        /// <summary> Получить информацию о созданных пользователем мероприятиях. </summary>
        /// <remarks> GET api/event/created </remarks> 
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
        /// <remarks> DELETE api/event/delete/id </remarks> 
        /// <param name="id"> Идентификатор мероприятия. </param>
        /// <returns> Мероприятие удалено. </returns>
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
        /// <remarks> 
        /// POST api/event/create 
        /// {
        ///     "title": String,
        ///     "description": String,
        ///     "eventDate": DateTime,
        ///     "address": String
        /// }
        /// </remarks> 
        /// <returns> Мероприятие создано. </returns>
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

        /// <summary> Редактировать мероприятие. </summary>
        /// <remarks> PUT api/event/update. </remarks>
        /// <param name="id"> Идентификатор события. </param> 
        /// <param name="request"> Информация для изменения. </param>
        /// <returns> Мероприятие изменено. </returns>
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
