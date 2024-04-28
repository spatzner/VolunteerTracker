using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Blazor.Models;
using VolunteerTracker.Blazor.Utilities;
using VolunteerTracker.Repository;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Services;

public class OrganizationDataProvider(VolunteerContext volunteerContext)
{
    public async Task<GridDataProviderResult<OrganizationGridModel>> Provide(GridDataProviderRequest<OrganizationGridModel> request)
    {
        IQueryable<Organization> query = volunteerContext
           .Organizations.Include(x => x.Address)
           .Include(x => x.MainPhone)
           .Include(x => x.Contact)
           .ThenInclude(c => c.Emails)
           .Include(x => x.Contact)
           .ThenInclude(c => c.Phones);

        foreach (FilterItem organizationFilter in request.Filters)
        {
            switch (organizationFilter.PropertyName)
            {
                case nameof(OrganizationGridModel.Name):
                    query = query.Where(p => EF.Functions.ILike(p.Name, $"{organizationFilter.Value}%"));
                    break;
            }
        }

        var countQuery = query;

        query = query.OrderBy(p => p.Name);

        query = query.Paginate(request.PageSize, request.PageNumber);

        return new GridDataProviderResult<OrganizationGridModel>
        {
            TotalCount = countQuery.AsNoTracking().Count(), 
            Data = await query.AsNoTracking().Select(p => new OrganizationGridModel(p)).ToListAsync()
        };
    }
}