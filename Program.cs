using HumanResourcesWebApi.ServiceExtensions;
using HumanResourcesWebApi.Common.Converters;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()); 
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((options) =>
{
    #region Documentaion Section

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HR API",
        Version = "v1",
        Description = "API documentation for HR App"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    #endregion
});

var connectionString = builder.Configuration.GetConnectionString("Network");

builder.Services.AddCustomRepositories(connectionString!);

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.DisplayRequestDuration();
});

app.UseStaticFiles();

app.UseCors(options =>
{
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
