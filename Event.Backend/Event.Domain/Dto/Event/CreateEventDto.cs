namespace Event.Domain.Dto.Event
{
    public class CreateEventDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } = DateTime.Now;
    }
}
