using System.Net;

namespace Event.Domain
{
    public class Response<T> : IResponse<T>
    {
        public T? Data { get; set; }

        public string? Description { get; set; }

        public HttpStatusCode Status { get; set; }
    }

    public interface IResponse<T>
    {
        T? Data { get; }
        string? Description { get; }
        HttpStatusCode Status { get; }
    }

}
