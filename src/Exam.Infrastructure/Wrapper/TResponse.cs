using System.Text.Json;

namespace Exam.Infrastructure.Wrapper
{
    public class TResponse<T>
    {
        public TResponse()
        {
        }
        public TResponse(T data,string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public TResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
