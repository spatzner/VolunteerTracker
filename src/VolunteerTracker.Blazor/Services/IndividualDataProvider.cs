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
                    query = query.Where(i => EF.Functions.ILike(i.FirstName, $"{individualFilter.Value}%"));
                    break;
                case nameof(IndividualGridModel.LastName):
                    query = query.Where(i => EF.Functions.ILike(i.LastName, $"{individualFilter.Value}%"));
                    break;
                case nameof(IndividualGridModel.Email):
                    query = query.Where(i => i.Emails.Any(e => EF.Functions.ILike(e.Address, $"{individualFilter.Value}%")));
                    break;
                case nameof(IndividualGridModel.Phone):
                    string search = GeneratedRegex.NonPhoneCharacter().Replace(individualFilter.Value, string.Empty);
                    query = query.Where(i => i.Phones.Any(ph => EF.Functions.ILike(ph.Number, $"{search}%")));
                    break;
            }
        }

        var countQuery = query;

        query = query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);

        query = query.Paginate(request.PageSize, request.PageNumber);

        return new GridDataProviderResult<IndividualGridModel>
        {
            TotalCount = countQuery.AsNoTracking().Count(), 
            Data = await query.AsNoTracking().Select(p => new IndividualGridModel(p)).ToListAsync()
        };
    }
}