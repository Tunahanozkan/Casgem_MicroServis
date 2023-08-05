using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccesful { get; set; }
        public List<string> Errors { get; set; }

        public static Response<T> Success(T data,int StatusCode)
        {
            return new Response<T> { Data = data, StatusCode = StatusCode ,IsSuccesful=true};
        }
        public static Response<T> Success(int StatusCode)
        {
            return new Response<T>
            {
                Data = default(T), 
                StatusCode = StatusCode, 
                IsSuccesful = true
            };
        }
        public static Response<T> Fail(List<string> errors,int StatusCode) 
        {
            return new Response<T>
            {
                Errors = errors,
                StatusCode = StatusCode,
                IsSuccesful = false
            };
        }
        public static Response<T> Fail(string error,int StatusCode)
        {
            return new Response<T>
            {
                Errors = new List<string>() { error },
                StatusCode = StatusCode,
                IsSuccesful = false
            };
        }
    }
}
