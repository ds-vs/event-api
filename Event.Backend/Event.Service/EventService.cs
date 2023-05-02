using System.Net;
using Event.Domain;
using Event.Domain.Dto.Event;
using Event.Domain.Entities;
using Event.Domain.Enums;
using Event.Domain.Repositories;
using Event.Domain.Repositories.Interfaces;
using Event.Service.Interfaces;

namespace Event.Service
{
    public class EventService: IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAccountRepository _accountRepository;

        public EventService(IEventRepository eventRepository, IAccountRepository accountRepository)
        {
            _eventRepository = eventRepository;
            _accountRepository = accountRepository;
        }

        public async Task<IResponse<GetEventDto>> CreateEventAsync(string login, CreateEventDto eventDto)
        {
            try
            {
                var user = await _accountRepository.GetAsync(login);

                var entity = new EventEntity()
                {
                    Title = eventDto.Title,
                    Description = eventDto.Description,
                    Responses = 0, // При создании события, количество откликов 0.
                    EventDate = eventDto.EventDate,
                    Status = StatusType.Actual,
                    AccountId = user.AccountId,
                };

                await _eventRepository.CreateAsync(entity);

                return new Response<GetEventDto>()
                {
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<GetEventDto>()
                {
                    Description = $"[CreateEventAsync] : {e.Message}",
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IResponse<GetEventDto>> DeleteEventAsync(string login, Guid id)
        {
            try
            {
                var user = await _accountRepository.GetAsync(login);

                var entity = _eventRepository.Get()
                    .FirstOrDefault(x => x.EventId == id && x.AccountId == user.AccountId);

                if (entity != null)
                {
                    await _eventRepository.DeleteAsync(entity.EventId);
                }
                else
                {
                    return new Response<GetEventDto>()
                    {
                        Description = "You cannot delete this event.",
                        Status = HttpStatusCode.OK,
                    };
                }

                return new Response<GetEventDto>()
                {
                    Description = "The event was successfully deleted. ",
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<GetEventDto>()
                {
                    Description = $"[DeleteEventAsync] : {e.Message}",
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }

        public IResponse<IEnumerable<GetEventDto>> Get()
        {
            try
            {
                // Получение актуальных мероприятий.
                var events = _eventRepository.Get()
                    .Where(x => x.Status == StatusType.Actual)
                    .Select(x => new GetEventDto
                    {
                        Title = x.Title,
                        Description = x.Description,
                        EventDate = x.EventDate,
                        Responses = x.Responses,
                        Status = x.Status,
                    });

                return new Response<IEnumerable<GetEventDto>>()
                {
                    Data = events,
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<GetEventDto>>()
                {
                    Description = $"[Get] : {e.Message}",
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IResponse<IEnumerable<GetEventDto>>> Get(string login)
        {
            try
            {
                var user = await _accountRepository.GetAsync(login);

                var events = _eventRepository.Get()
                    .Where(x => x.AccountId == user.AccountId)
                    .Select(x => new GetEventDto
                    {
                        Title = x.Title,
                        Description = x.Description,
                        EventDate = x.EventDate,
                        Responses = x.Responses,
                        Status = x.Status,
                    });

                return new Response<IEnumerable<GetEventDto>>()
                {
                    Data = events,
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<GetEventDto>>()
                {
                    Description = $"[Get] : {e.Message}",
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }

        public IResponse<GetEventDto> Get(Guid id)
        {
            try
            {
                var eventEntity = _eventRepository.Get()
                    .FirstOrDefault(x => x.EventId == id);

                if (eventEntity != null)
                {
                    var eventDto = new GetEventDto
                    {
                        Title = eventEntity.Title,
                        EventDate = eventEntity.EventDate,
                        Description = eventEntity.Description,
                        Responses = eventEntity.Responses,
                        Status = eventEntity.Status,
                    };

                    return new Response<GetEventDto>()
                    {
                        Data = eventDto,
                        Status = HttpStatusCode.OK,
                    };
                }

                return new Response<GetEventDto>()
                {
                    Description = "Event not found.",
                    Status = HttpStatusCode.NotFound,
                };
            }
            catch (Exception e)
            {
                return new Response<GetEventDto>()
                {
                    Description = $"[Get] : {e.Message}",
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<IResponse<GetEventDto>> UpdateEventAsync(string login, Guid id, CreateEventDto eventDto)
        {
            try
            {
                var user = await _accountRepository.GetAsync(login);

                var entity = _eventRepository.Get()
                    .FirstOrDefault(x => x.EventId == id && x.AccountId == user.AccountId);

                if (entity != null)
                {
                    entity.Title = eventDto.Title;
                    entity.Description = eventDto.Description;
                    entity.EventDate = eventDto.EventDate;

                    await _eventRepository.UpdateAsync(entity);
                }
                else
                {
                    return new Response<GetEventDto>()
                    {
                        Description = "You cannot change this event.",
                        Status = HttpStatusCode.OK,
                    };
                }

                return new Response<GetEventDto>()
                {
                    Description = "Event changed successfully.",
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<GetEventDto>()
                {
                    Description = $"[UpdateEventAsync] : {e.Message}",
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task UpdateEventStatusAsync()
        {
            try
            {
                var events = _eventRepository.Get()
                    .Where(e => e.EventDate < DateTime.Now && e.Status == StatusType.Actual);

                await _eventRepository.UpdateEventStatusAsync(events);
            }
            catch (Exception e)
            {
                throw new Exception($"[UpdateEventStatusAsync]: {e.Message}");
            }
        }
    }
}
