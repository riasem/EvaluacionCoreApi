

namespace EvaluacionCore.Application.Common.Wrappers
{
    public class ResponseType<T>
    {
        public ResponseType()
        {

        }

        public ResponseType(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public ResponseType(T data, string message = null)
        {
            Succeeded = true; 
            Message = message;
            Data = data;
        }

        public bool Succeeded { get; set; } 
        public string Message { get; set; }
        public string StatusCode { get; set; }

        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

        public T Data { get; set; }

    }
}
