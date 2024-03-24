using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Blazor.Utilities;
using VolunteerTracker.Repository.Entities;
using VolunteerTracker.Repository;

namespace VolunteerTracker.Blazor.Services
{
    public class PersonsProvider(VolunteerContext volunteerContext)
    {
        public async Task<GridDataProviderResult<Person>> PersonsDataProvider(GridDataProviderRequest<Person> request)
        {
            IQueryable<Person> query = volunteerContext.Persons;

            query = request.Filters.Aggregate(query, (current, requestFilter) => current.BootstrapFilterEF(requestFilter));

            var countQuery = query;

            IOrderedQueryable<Person>? orderedQuery = null;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (request.Sorting != null)
                orderedQuery = request.Sorting.Aggregate(orderedQuery,
                    (current, sort) => sort.SortDirection switch
                    {
                        SortDirection.Ascending when current == null => query.OrderBy(p => EF.Property<object>(p, sort.SortString)),
                        SortDirection.Ascending => current.ThenBy(p => EF.Property<object>(p, sort.SortString)),
                        SortDirection.Descending when current == null => query.OrderByDescending(p => EF.Property<object>(p, sort.SortString)),
                        SortDirection.Descending => current.ThenByDescending(p => EF.Property<object>(p, sort.SortString)),
                        _ => current
                    });

            orderedQuery ??= query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);

            query = orderedQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

            var result = new GridDataProviderResult<Person> { TotalCount = countQuery.AsNoTracking().Count(), Data = await query.AsNoTracking().ToListAsync() };

            return result;
        }
    }
}