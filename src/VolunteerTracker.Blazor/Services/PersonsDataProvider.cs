using System.Collections;
using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Blazor.Utilities;
using VolunteerTracker.Repository.Entities;
using VolunteerTracker.Repository;

namespace VolunteerTracker.Blazor.Services
{
    public class PersonsDataProvider(VolunteerContext volunteerContext)
    {
        public async Task<GridDataProviderResult<Person>> Provide(GridDataProviderRequest<Person> request)
        {
            IQueryable<Person> query = volunteerContext.Persons;

            query = request.Filters.Aggregate(query, (current, requestFilter) => current.BootstrapFilterEF(requestFilter));

            var countQuery = query;

            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract - request.Sorting is null on initial load
            var sorting = request.Sorting?.Where(s => s.SortDirection != SortDirection.None).ToList();
            
            query = sorting != null && sorting.Count != 0 
                ? query.BootstrapSortEF(sorting) 
                : query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);

            query = query.BootstrapPaginateEF(request.PageSize, request.PageNumber);
            
            return new GridDataProviderResult<Person>
            {
                TotalCount = countQuery.AsNoTracking().Count(),
                Data = await query.AsNoTracking().ToListAsync()
            };
        }
    }

    public class Individual(Person person)
    {
        public Guid Id => person.Id;
        public string FullName  => person.ToString();
        public string Location => person.Address.ToString();
        public string? Phone => person.Phones.FirstOrDefault(x => x.IsPrimary)?.Number;
        public string? Email => person.Emails.FirstOrDefault(x => x.IsPrimary)?.Address;
    }
    
    
    public class IndividualDataProvider(VolunteerContext volunteerContext)
    {
        public async Task<GridDataProviderResult<Individual>> Provide(GridDataProviderRequest<Individual> request)
        {
            IQueryable<Person> query = volunteerContext.Persons.Include(x => x.Address).Include(x => x.Emails).Include(x => x.Phones);

            query = request.Filters.Aggregate(query, (current, requestFilter) => current.BootstrapFilterEF(requestFilter));

            var countQuery = query;

            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract - request.Sorting is null on initial load
            //var sorting = request.Sorting?.Where(s => s.SortDirection != SortDirection.None).ToList();

            //query = sorting != null && sorting.Count != 0
            //    ? query.BootstrapSortEF(sorting)
            //    : query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);

            query = query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
            
            query = query.BootstrapPaginateEF(request.PageSize, request.PageNumber);

            return new GridDataProviderResult<Individual>
            {
                TotalCount = countQuery.AsNoTracking().Count(),
                Data = await query.AsNoTracking().Select(p => new Individual(p)).ToListAsync()
            };
        }
        
        
    }
}