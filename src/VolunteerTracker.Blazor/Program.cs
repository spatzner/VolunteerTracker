using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Blazor.Components;
using VolunteerTracker.Blazor.Components.Account;
using VolunteerTracker.Blazor.Data;
using VolunteerTracker.Blazor.Services;
using VolunteerTracker.Blazor.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

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

var databaseSettings = new VolunteerDatabaseSettings();
builder
   .Configuration.GetSection("VolunteerDatabaseSettings")
   .Bind(databaseSettings, options => options.ErrorOnUnknownConfiguration = true);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseSettings.ConnectionString));

builder
   .Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddSignInManager()
   .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();

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