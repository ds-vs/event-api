using Event.Domain.Dto.Email;

namespace Event.Service.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
