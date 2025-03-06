namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class PaginatedList<T>(IReadOnlyCollection<T> data, int totalItems, int currentPage, int pageSize)
{
    public IReadOnlyCollection<T> Data { get; } = data;
    public int CurrentPage { get; } = currentPage;
    public int TotalPages { get; } = (int)Math.Ceiling(totalItems / (double)pageSize);
    public int TotalItems { get; } = totalItems;
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}