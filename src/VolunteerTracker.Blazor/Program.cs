using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Blazor.Components;
using VolunteerTracker.Blazor.Components.Account;
using VolunteerTracker.Blazor.Data;
using VolunteerTracker.Blazor.Services;
using VolunteerTracker.Blazor.Settings;
using VolunteerTracker.Common;
using VolunteerTracker.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// Add services to the container.
builder.Services.AddRazorComponents(options =>
    options.DetailedErrors = builder.Environment.IsDevelopment()).AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder
   .Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
   .AddIdentityCookies();

var volunteerDatabaseSettings = new VolunteerDatabaseSettings();
builder
   .Configuration.GetSection("VolunteerDatabaseSettings")
   .Bind(volunteerDatabaseSettings,
        options =>
        {
            options.ErrorOnUnknownConfiguration = true;
        });

var aspNetDatabaseSettings = new AspNetDatabaseSettings();
builder
   .Configuration.GetSection("AspNetDatabaseSettings")
   .Bind(aspNetDatabaseSettings, options => options.ErrorOnUnknownConfiguration = true);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(aspNetDatabaseSettings.ConnectionString));
//builder.Services.AddDbContext<VolunteerContext>(options => options.UseNpgsql(volunteerDatabaseSettings.ConnectionString), ServiceLifetime.Scoped);
builder.Services.AddDbContextFactory<VolunteerContext>(options =>
{
    options.UseNpgsql(volunteerDatabaseSettings.ConnectionString);
});

builder
   .Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddSignInManager()
   .AddDefaultTokenProviders();

builder.Services.AddBlazorBootstrap();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();
builder.Services.AddTransient<PersonsDataProvider>();
builder.Services.AddTransient<IndividualDataProvider>();
builder.Services.AddTransient<PhoneNumberFormatter>();


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();