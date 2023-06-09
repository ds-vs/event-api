﻿using Event.Domain.Enums;

namespace Event.Domain.Dto.Event
{
    public class GetEventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long Responses { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public StatusType Status { get; set; }
    }
}
