using System.Linq.Dynamic.Core;

namespace Acacia.Core.Helpers
{
    public static class QueryFilterHelper
    {
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, Dictionary<string, string> filters)
        {
            if (filters == null || !filters.Any()) return query;

            foreach (var filter in filters)
            {
                query = query.Where($"{filter.Key}.ToString().Contains(@0)", filter.Value);
            }

            return query;
        }

        public static IQueryable<T> ApplyDateRange<T>(this IQueryable<T> query, DateTime? from, DateTime? to)
            where T : class
        {
            if (from.HasValue)
                query = query.Where("CreationDate >= @0", from.Value);

            if (to.HasValue)
                query = query.Where("CreationDate <= @0", to.Value);

            return query;
        }
    }
}
