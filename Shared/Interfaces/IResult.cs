using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Interfaces;

public interface IResult<T>
{
    public List<string> Messages { get; set; }
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public Exception? Exception { get; set; }
    public string? Token { get; set; }
}
