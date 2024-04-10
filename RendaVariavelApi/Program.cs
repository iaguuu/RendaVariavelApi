using Microsoft.EntityFrameworkCore;
using RendaVariavelApi.Data;
using RendaVariavelApi.Helpers;
using RendaVariavelApi.Repositories;
using RendaVariavelApi.Repositories.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    //options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<RendaVariavelDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
    );

builder.Services.AddScoped<IFundoImobiliarioRepositorio, FundoImobiliarioRepositorio>();
builder.Services.AddScoped<ICotacaoRepositorio, CotacaoRepositorio>();
builder.Services.AddScoped<IDividendoRepositorio, DividendoRepositorio>();


var app = builder.Build();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ExceptionHandling(loggerFactory);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
