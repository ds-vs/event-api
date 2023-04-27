using Event.Domain.Enums;

namespace Event.Domain
{
    /// <summary> Ответ возвращаемый из методов сервисов. </summary>
    public class Response<T> : IResponse<T>
    {
        public T? Data { get; set; }

        public string? Description { get; set; }

        public ResponseCode Status { get; set; }
    }

    public interface IResponse<T>
    {
        T? Data { get; }
        string? Description { get; }
        ResponseCode Status { get; }
    }

}
