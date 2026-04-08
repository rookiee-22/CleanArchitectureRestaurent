using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared;

public class Result<T> : IResult<T>
{
    public List<string> Messages { get; set; }
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public Exception? Exception { get; set; }
    public string? Token { get; set; }
  

    public static Result<T> Success(string message)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Messages = new List<string> { message },
            Data = default(T),
            StatusCode = 200
        };
    }

    public static Result<T> Success(T data, string message)
    {
        return new Result<T>
        {
            IsSuccess= true,
            Messages= new List<string> { message },
            Data= data,
            StatusCode = 200
        };
    }


    public static Result<T> Success(T data, string token, string message)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Messages = new List<string> { message },
            Data = data,
            Token = token,
            StatusCode=200
        };
    }
    public static Result<T> BadRequest(string message)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Messages = new List<string> { message },
            Data=default(T),
            StatusCode=400
        };
    }
}
