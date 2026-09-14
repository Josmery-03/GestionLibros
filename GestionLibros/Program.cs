using GestionLibros.DAL;
using Microsoft.EntityFrameworkCore;
using GestionLibros.Services;
using GestionLibros.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Inyectar el contexto
var ConStr = builder.Configuration.GetConnectionString("SqlConStr");

//Agregar contexto al builder con el ConStr

builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));

// Inyectar libros services
builder.Services.AddScoped<LibrosServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
