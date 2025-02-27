using Microsoft.EntityFrameworkCore;
using ProgramSystemLibrary.DAL;
using ProgramSystemLibrary.BLL;
using ProgramSystemWeb.Components;
using ProgramSystemLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
    
// Call extenson method to inject database context and services.
var connectionString = builder.Configuration.GetConnectionString("StarTEDDb");
builder.Services.StarTEDExtensionServices(options => options.UseSqlServer(connectionString));

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
