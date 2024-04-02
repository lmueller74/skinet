using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SQLitePCL;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse()
        {
        }

        public ApiResponse(int statusCode)
        {
            StatusCode = statusCode; 
            Message  = GetDefaultMessageForStatusCode(statusCode);
        }
        public ApiResponse(int statusCode, string message)
        {
            StatusCode = statusCode; 
            Message  = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode {get;set;}
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            string? s = statusCode switch
            {
                400 => "a bad request you have made",
                401 => "aut you are not",
                404 => "resource not found it was",
                500 => "",
                _ => null
            };

            return s ?? String.Empty;
        }
       
    }
}