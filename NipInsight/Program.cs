using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NipInsight.Application.Services;
using NipInsight.Domain.Interfaces.Clients;
using NipInsight.Domain.Interfaces.Repositories;
using NipInsight.Domain.Interfaces.Services;
using NipInsight.Infrastructure.Clients;
using NipInsight.Infrastructure.Data;
using NipInsight.Infrastructure.Middleware;
using NipInsight.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(opt =>
    {
        opt.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IProcessCompanyDataService, ProcessCompanyDataService>();

builder.Services.AddHttpClient<IWlApiClient, WlApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WlApi:BasePath"));
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors();

app.MapGet("/hello", () => "Hello");

app.MapGet("/nip/{nip:regex(^\\d{{10}}$)}",
    async ([FromQuery] DateTime date, string nip, IProcessCompanyDataService processCompanyDataService) =>
    {
        var response = await processCompanyDataService.GetAndStoreCompanyData(nip, date);
        return response.Success ? Results.Ok(response.Result) : Results.NotFound(response.ErrorMessage);
    }).WithName("GetCompanyInformation");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();