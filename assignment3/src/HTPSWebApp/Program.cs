using HTPSWebApp.Components;
using Microsoft.EntityFrameworkCore;
using HTPSSystem;

var builder = WebApplication.CreateBuilder(args);

var connectstring = builder.Configuration.GetConnectionString("HTPSDB");
builder.Services.HTPSDependencies(options => options.UseSqlServer(connectstring));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
