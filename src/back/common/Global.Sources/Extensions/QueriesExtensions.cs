using Global.Sources.Constants;

namespace Global.Sources.Extensions;

public static class QueriesFilters
{
    public static IQueryable<T> Paginate<T>(
        this IQueryable<T> query, 
        int pageNumber = 1, 
        int pageSize = GeneralConstants.DefaultPageSize) 
        where T : class
        => query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable();
}