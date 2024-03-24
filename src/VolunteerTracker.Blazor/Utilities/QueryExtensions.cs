using System.Reflection;
using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;

namespace VolunteerTracker.Blazor.Utilities;

public static class QueryExtensions
{
    public static IQueryable<T> BootstrapFilterEF<T>(this IQueryable<T> query, FilterItem filter)
    {
        if (filter.Operator is FilterOperator.Clear or FilterOperator.None)
            return query;

        PropertyInfo prop = typeof(T).GetProperty(filter.PropertyName)
         ?? throw new ArgumentException($"Property {filter.PropertyName} not found on type {typeof(T).Name}");

        return prop.PropertyType switch
        {
            { } t when t == typeof(DateTime) => FilterDateTime(query, filter),
            { } t when t == typeof(string) => FilterString(query, filter),
            { } t when t == typeof(int) => FilterInt(query, filter),
            { } t when t == typeof(decimal) => FilterDecimal(query, filter),
            _ => query
        };
    }

    private static IQueryable<T> FilterDecimal<T>(IQueryable<T> query, FilterItem filter)
    {
        return filter.Operator switch
        {
            FilterOperator.Equals =>
                query.Where(p => p != null && Equals(EF.Property<DateTime>(p, filter.PropertyName), DateTime.Parse(filter.Value))),
            FilterOperator.NotEquals =>
                query.Where(p => p != null && !Equals(EF.Property<DateTime>(p, filter.PropertyName), DateTime.Parse(filter.Value))),
            FilterOperator.LessThan =>
                query.Where(p => p != null && EF.Property<DateTime>(p, filter.PropertyName) < DateTime.Parse(filter.Value)),
            FilterOperator.LessThanOrEquals =>
                query.Where(p => p != null && EF.Property<DateTime>(p, filter.PropertyName) <= DateTime.Parse(filter.Value)),
            FilterOperator.GreaterThan =>
                query.Where(p => p != null && EF.Property<DateTime>(p, filter.PropertyName) > DateTime.Parse(filter.Value)),
            FilterOperator.GreaterThanOrEquals =>
                query.Where(p => p != null && EF.Property<DateTime>(p, filter.PropertyName) >= DateTime.Parse(filter.Value)),
            _ => throw new ArgumentOutOfRangeException(nameof(filter.Operator), filter.Operator, null)
        };
    }
    private static IQueryable<T> FilterDateTime<T>(IQueryable<T> query, FilterItem filter)
    {
        if (filter.Value == string.Empty)
        {
            return filter.Operator switch
            {
                FilterOperator.IsNull => query.Where(p => p != null && EF.Property<decimal?>(p, filter.PropertyName) == null),
                FilterOperator.IsNotNull => query.Where(p => p != null && EF.Property<decimal?>(p, filter.PropertyName) != null),
                _ => query
            };
        }

        if (decimal.TryParse(filter.Value, out decimal value))
            return query.Where(x => false);
            
        return filter.Operator switch
        {
            FilterOperator.Equals =>
                query.Where(p => p != null && Equals(EF.Property<DateTime>(p, filter.PropertyName), DateTime.Parse(filter.Value))),
            FilterOperator.NotEquals =>
                query.Where(p => p != null && !Equals(EF.Property<DateTime>(p, filter.PropertyName), DateTime.Parse(filter.Value))),
            FilterOperator.LessThan =>
                query.Where(p => p != null && EF.Property<DateTime>(p, filter.PropertyName) < DateTime.Parse(filter.Value)),
            FilterOperator.LessThanOrEquals =>
                query.Where(p => p != null && EF.Property<DateTime>(p, filter.PropertyName) <= DateTime.Parse(filter.Value)),
            FilterOperator.GreaterThan =>
                query.Where(p => p != null && EF.Property<DateTime>(p, filter.PropertyName) > DateTime.Parse(filter.Value)),
            FilterOperator.GreaterThanOrEquals =>
                query.Where(p => p != null && EF.Property<DateTime>(p, filter.PropertyName) >= DateTime.Parse(filter.Value)),
            _ => throw new ArgumentOutOfRangeException(nameof(filter.Operator), filter.Operator, null)
        };
    }
    private static IQueryable<T> FilterInt<T>(IQueryable<T> query, FilterItem filter)
    {
        if (filter.Value == null)
        {
            return filter.Operator switch
            {
                FilterOperator.IsNull => query.Where(p => p != null && EF.Property<int?>(p, filter.PropertyName) == null),
                FilterOperator.IsNotNull => query.Where(p => p != null && EF.Property<int?>(p, filter.PropertyName) != null),
                _ => query
            };
        }

        if (int.TryParse(filter.Value, out int value))
            return query.Where(x => false);

        return filter.Operator switch
        {
            FilterOperator.Equals =>
                query.Where(p => p != null && Equals(EF.Property<int>(p, filter.PropertyName), value)),
            FilterOperator.NotEquals =>
                query.Where(p => p != null && !Equals(EF.Property<int>(p, filter.PropertyName), value)),
            FilterOperator.LessThan =>
                query.Where(p => p != null && EF.Property<int>(p, filter.PropertyName) < value),
            FilterOperator.LessThanOrEquals =>
                query.Where(p => p != null && EF.Property<int>(p, filter.PropertyName) <= value),
            FilterOperator.GreaterThan =>
                query.Where(p => p != null && EF.Property<int>(p, filter.PropertyName) > value),
            FilterOperator.GreaterThanOrEquals =>
                query.Where(p => p != null && EF.Property<int>(p, filter.PropertyName) >= value),
            _ => throw new ArgumentOutOfRangeException(nameof(filter.Operator), filter.Operator, null)
        };
    }

    private static IQueryable<T> FilterString<T>(IQueryable<T> query, FilterItem filter)
    {
        return filter.Operator switch
        {
            FilterOperator.Equals => query.Where(p => p != null && EF.Property<string>(p, filter.PropertyName).Equals(filter.Value)),
            FilterOperator.NotEquals => query.Where(p => p != null && !EF.Property<string>(p, filter.PropertyName).Equals(filter.Value)),
            FilterOperator.Contains => query.Where(p => p != null && EF.Property<string>(p, filter.PropertyName).Contains(filter.Value)),
            FilterOperator.StartsWith => query.Where(p => p != null && EF.Property<string>(p, filter.PropertyName).StartsWith(filter.Value)),
            FilterOperator.EndsWith => query.Where(p => p != null && EF.Property<string>(p, filter.PropertyName).EndsWith(filter.Value)),
            FilterOperator.DoesNotContain => query.Where(p => p != null && !EF.Property<string>(p, filter.PropertyName).Contains(filter.Value)),
            FilterOperator.IsNull => query.Where(p => p != null && EF.Property<string?>(p, filter.PropertyName) == null),
            FilterOperator.IsEmpty => query.Where(p => p != null && EF.Property<string>(p, filter.PropertyName) == string.Empty),
            FilterOperator.IsNotNull => query.Where(p => p != null && EF.Property<string?>(p, filter.PropertyName) != null),
            FilterOperator.IsNotEmpty => query.Where(p => p != null && EF.Property<string>(p, filter.PropertyName).Length > 0),
            _ => throw new ArgumentOutOfRangeException(nameof(filter.Operator), filter.Operator, null)
        };
    }
}