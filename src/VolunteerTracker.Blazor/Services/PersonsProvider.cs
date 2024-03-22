using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Repository.Entities;
using VolunteerTracker.Repository;

namespace VolunteerTracker.Blazor.Services
{
    public class PersonsProvider(VolunteerContext volunteerContext)
    {
        public async Task<GridDataProviderResult<Person>> PersonsDataProvider(GridDataProviderRequest<Person> request)
        {
            IQueryable<Person> query = volunteerContext.Persons.AsNoTracking();

            foreach (FilterItem filter in request.Filters)
            {
                
                query = BootstrapFilterEF(query, filter);
            }

            IOrderedQueryable<Person>? orderedQuery = null;

            if(request.Sorting != null)
                foreach (SortingItem<Person> sort in request.Sorting)
                {
                    if (sort.SortDirection == SortDirection.Ascending)
                    {
                        orderedQuery = orderedQuery == null ? query.OrderBy(p => EF.Property<object>(p, sort.SortString)) : orderedQuery.ThenBy(p => EF.Property<object>(p, sort.SortString));
                    }
                    else if (sort.SortDirection == SortDirection.Descending)
                    {
                        orderedQuery = orderedQuery == null ? query.OrderByDescending(p => EF.Property<object>(p, sort.SortString)) : orderedQuery.ThenByDescending(p => EF.Property<object>(p, sort.SortString));
                    }
                }

            orderedQuery ??= query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);

            query = orderedQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

            try
            {

                var result = new GridDataProviderResult<Person> { TotalCount = volunteerContext.Persons.Count(), Data = await query.ToListAsync() };


                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return new GridDataProviderResult<Person>();
        }

        private static IQueryable<T> BootstrapFilterEF<T>(IQueryable<T> query, FilterItem filter)
        {
            if (filter?.Value == null || filter.Operator == FilterOperator.Clear || filter.Operator == FilterOperator.None)
            {
                return query;
            }
            
            var prop = typeof(T).GetProperty(filter.PropertyName);

            if (prop == null)
            {
                return query;
            }
            
            if (prop.PropertyType == typeof(DateTime))
            {
                switch (filter.Operator)
                {
                    case FilterOperator.Equals:
                        break;
                    case FilterOperator.NotEquals:
                        break;
                    case FilterOperator.LessThan:
                        break;
                    case FilterOperator.LessThanOrEquals:
                        break;
                    case FilterOperator.GreaterThan:
                        break;
                    case FilterOperator.GreaterThanOrEquals:
                        break;
                    case FilterOperator.Contains:
                        break;
                    case FilterOperator.StartsWith:
                        break;
                    case FilterOperator.EndsWith:
                        break;
                    case FilterOperator.DoesNotContain:
                        break;
                    case FilterOperator.IsNull:
                        break;
                    case FilterOperator.IsEmpty:
                        break;
                    case FilterOperator.IsNotNull:
                        break;
                    case FilterOperator.IsNotEmpty:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                return query.Where(p => EF.Property<DateTime>(p, filter.PropertyName) == DateTime.Parse(filter.Value));
            }

            if (prop.PropertyType == typeof(string))
            {
                return FilterString(query, filter);
            }

            if (prop.PropertyType == typeof(int))
            {
                return query.Where(p => EF.Property<int>(p, filter.PropertyName) == int.Parse(filter.Value));
            }
            
            return query;
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
                _ => query
            };
        }
    }
}
