namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class PageListRequest
{
    public string? Order { get; set; }
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}