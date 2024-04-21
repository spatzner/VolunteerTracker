using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Blazor.Utilities;
using VolunteerTracker.Repository.Entities;
using VolunteerTracker.Repository;

namespace VolunteerTracker.Blazor.Services
{
    public class IndividualsDataProvider(VolunteerContext volunteerContext)
    {
        public async Task<GridDataProviderResult<Individual>> Provide(GridDataProviderRequest<Individual> request)
        {
            IQueryable<Individual> query = volunteerContext.Individuals;

            query = request.Filters.Aggregate(query, (current, requestFilter) => current.BootstrapFilterEF(requestFilter));

            var countQuery = query;

            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract - request.Sorting is null on initial load
            var sorting = request.Sorting?.Where(s => s.SortDirection != SortDirection.None).ToList();

            query = sorting != null && sorting.Count != 0 
                ? query.BootstrapSortEF(sorting) 
                : query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);

            query = query.BootstrapPaginateEF(request.PageSize, request.PageNumber);

            return new GridDataProviderResult<Individual>
            {
                TotalCount = countQuery.AsNoTracking().Count(), 
                Data = await query.AsNoTracking().ToListAsync()
            };
        }
    }
}