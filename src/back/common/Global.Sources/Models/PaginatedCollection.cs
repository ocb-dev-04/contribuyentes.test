namespace Global.Sources.Models;

public sealed record PaginatedCollection<T>(
    IEnumerable<T> Data,
    long TotalItems,
    long TotalPages,
    bool IsLastPage)
{
    public static PaginatedCollection<T> Map(
        IEnumerable<T> data,
        long totalItems,
        long totalPages,
        bool isLastPage)
        => new(
            data,
            totalItems,
            totalPages,
            isLastPage);

    public PaginatedCollection<TOut> MapData<TOut>(Func<T, TOut> converter)
        => new(
            Data.Select(converter),
            TotalItems,
            TotalPages,
            IsLastPage);

    public static PaginatedCollection<T> Empty()
        => new(
            Enumerable.Empty<T>(),
            0L,
            0L,
            true);
}
