using CinemaTicketing.API;
using CinemaTicketing.API.Common.Errors;
using CinemaTicketing.API.Extensions;
using CinemaTicketing.Application;
using CinemaTicketing.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddSingleton<ProblemDetailsFactory, CinemaTicketingProblemDetailsFactory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.ApplyMigrations();

if (app.Environment.IsStaging())
    app.Services.EnsureTestDbIsCreated();

app.UseExceptionHandler("/error");
app.UseHsts();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();