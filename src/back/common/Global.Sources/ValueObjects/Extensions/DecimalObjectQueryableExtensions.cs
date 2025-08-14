using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Global.Sources.ValueObjects.Values;

namespace Global.Sources.ValueObjects.Extensions;

public static class DecimalObjectQueryableExtensions
{
    public static async Task<decimal> SumDecimalObjectAsync<T>(
        this IQueryable<T> query,
        Expression<Func<T, DecimalObject>> selector,
        CancellationToken cancellationToken = default)
        where T : class
    {
        ParameterExpression param = selector.Parameters[0];
        Expression member = selector.Body;

        string columnName = (member as MemberExpression)?.Member.Name
                         ?? throw new InvalidOperationException("Selector must be a member expression");

        MethodCallExpression efPropertyCall = Expression.Call(
                typeof(EF),
                nameof(EF.Property),
                new Type[] { typeof(decimal) },
                param,
                Expression.Constant(columnName)
            );

        Expression<Func<T, decimal>> lambda = Expression.Lambda<Func<T, decimal>>(efPropertyCall, param);

        return await query.SumAsync(lambda, cancellationToken);
    }
}
