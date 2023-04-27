using System.Net;
using Event.Domain;
using Event.Domain.Entities;
using Event.Domain.Enums;
using Event.Domain.Repositories.Interfaces;
using Event.Service.Interfaces;

namespace Event.Service
{
    public class EventService: IEventService
    {
        private readonly IEventRepository _repository;

        public EventService(IEventRepository repository)
        {
            _repository = repository;
        }

        public IResponse<IEnumerable<EventEntity>> Get()
        {
            try
            {
                // Получение актуальных мероприятий.
                var events = _repository.Get()
                    .Where(x => x.Status == StatusType.Actual);

                return new Response<IEnumerable<EventEntity>>()
                {
                    Data = events,
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<EventEntity>>()
                {
                    Description = $"[Get] : {e.Message}",
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}
