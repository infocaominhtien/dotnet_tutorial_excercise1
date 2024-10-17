using Microsoft.OpenApi.Models;
using WebApplication7;
using WebApplication7.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigHelper.Init(builder.Configuration);

builder.Services.ConfigureJsonOptions();
builder.Services.ConfigureMySql();
builder.Services.ConfigureServices();
builder.Services.AddSwaggerGen(options =>
{
    // enable comment on controller
    // includeControllerXmlComments: true
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Tutorial Exercise 1 API",
        Description = "An ASP.NET Core Web API for managing Tutorial Exercise 1 items"
    });
});

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