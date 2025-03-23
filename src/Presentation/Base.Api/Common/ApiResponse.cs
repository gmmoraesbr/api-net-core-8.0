﻿namespace Base.Api.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public static ApiResponse<T> SuccessResponse(T? data, string? message = null)
        => new ApiResponse<T> { Success = true, Message = message, Data = data };

    public static ApiResponse<T> FailResponse(string message, IEnumerable<string>? errors = null)
        => new ApiResponse<T> { Success = false, Message = message, Errors = errors };
}


