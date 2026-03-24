using System.Linq.Expressions;

namespace HospitalManagementSystemAPIVersion;

public static class Extension
{


    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> expression)
    {
        if (condition)
            return query.Where(expression);

        return query;
    }

}