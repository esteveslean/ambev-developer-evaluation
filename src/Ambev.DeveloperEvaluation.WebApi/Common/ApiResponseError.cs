﻿
namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiResponseError
{
    public string Type { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
}
