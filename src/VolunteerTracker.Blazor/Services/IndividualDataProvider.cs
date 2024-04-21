using BlazorBootstrap;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Blazor.Models;
using VolunteerTracker.Blazor.Utilities;
using VolunteerTracker.Common;
using VolunteerTracker.Repository;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Services;

public class IndividualDataProvider(VolunteerContext volunteerContext)
{
    public async Task<GridDataProviderResult<IndividualGridModel>> Provide(GridDataProviderRequest<IndividualGridModel> request)
    {
        IQueryable<Individual> query = volunteerContext.Individuals.Include(x => x.Address).Include(x => x.Emails).Include(x => x.Phones);

        foreach (FilterItem individualFilter in request.Filters)
        {
            switch (individualFilter.PropertyName)
            {
                case nameof(IndividualGridModel.FirstName):
                    query = query.Where(p => EF.Functions.ILike(p.FirstName, $"{individualFilter.Value}%"));
                    break;
                case nameof(IndividualGridModel.LastName):
                    query = query.Where(p => EF.Functions.ILike(p.LastName, $"{individualFilter.Value}%"));
                    break;
                case nameof(IndividualGridModel.Email):
                    query = query.Where(p => p.Emails.Any(e => EF.Functions.ILike(e.Address, $"{individualFilter.Value}%")));
                    break;
                case nameof(IndividualGridModel.Phone):
                    string search = GeneratedRegex.NonPhoneCharacter().Replace(individualFilter.Value, string.Empty);
                    query = query.Where(p => p.Phones.Any(ph => EF.Functions.ILike(ph.Number, $"{search}%")));
                    break;
            }
        }

        var countQuery = query;

        query = query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);

        query = query.BootstrapPaginateEF(request.PageSize, request.PageNumber);

        return new GridDataProviderResult<IndividualGridModel>
        {
            TotalCount = countQuery.AsNoTracking().Count(), 
            Data = await query.AsNoTracking().Select(p => new IndividualGridModel(p)).ToListAsync()
        };
    }
}