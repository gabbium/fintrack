namespace CleanArch.Persistence.EFCore;

public static class PaginatedListEfExtensions
{
    public static async Task<PaginatedList<TDestination>> ToPaginatedListAsync<TSource, TDestination>(
        this IQueryable<TSource> queryable,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TDestination>> selector,
        CancellationToken cancellationToken = default)
    {
        var totalItems = await queryable.CountAsync(cancellationToken);

        var items = await queryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(selector)
            .ToListAsync(cancellationToken);

        return new PaginatedList<TDestination>(items, totalItems, pageNumber, pageSize);
    }
}
