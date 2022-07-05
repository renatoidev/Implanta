using FluentValidation.AspNetCore;
using Implanta.Data.Context;
using Implanta.Domain.Contracts;
using Implanta.Infra.Repository;
using Implanta.Service.Interfaces;
using Implanta.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ImplantaDataContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddScoped<IProfissionalService, ProfissionalServices>();
    builder.Services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
}