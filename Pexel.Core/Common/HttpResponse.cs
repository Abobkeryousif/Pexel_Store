using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Core.Common
{
    public class HttpResponse<T> where T : class
    {
        public HttpResponse(HttpStatusCode httpStatusCode , string message , T data)
        {
            StatusCode = (int)httpStatusCode;
            Message = message;
            Data = data;
        }
        public HttpResponse(HttpStatusCode httpStatusCode , string message)
        {
            StatusCode = (int)httpStatusCode;
            Message = message;
        }


        public int StatusCode { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }
    }
}
