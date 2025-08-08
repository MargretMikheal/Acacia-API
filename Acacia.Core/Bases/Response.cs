using System.Net;

namespace Acacia.Core.Bases
{
    public class Response<T>
    {
        public Response() { }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message, bool succeeded = false)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public Dictionary<string, List<string>> Errors { get; set; }

        //public object Guard { get; set; } = null;

        public HttpStatusCode Response_Code { get; set; }
    }

}
