using Microsoft.AspNetCore.Authentication.JwtBearer;
using HumanResourcesWebApi.Repository.Dapper;
using HumanResourcesWebApi.ServiceExtensions;
using HumanResourcesWebApi.Common.Converters;
using HumanResourcesWebApi.Abstract;
using Microsoft.OpenApi.Models;
using HumanResourcesWebApi.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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

    #region Jwt Bearer Section
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Jwt Authentication",
        Description = "Type in a valid JWT Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "Jwt",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme,Array.Empty<string>() }
                });

    #endregion

});


#region Anviz ConnectionString

var anvizConnectionString = builder.Configuration.GetConnectionString("AnvizConnectionString");
builder.Services.AddScoped<IAnvizRepository, AnvizRepository>(provider => new AnvizRepository(anvizConnectionString!));

#endregion

#region Jwt Section

var jwtOptions = builder.Configuration.GetSection("JwtOptions");
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions["Key"]!));

const int ACCESS_TOKEN_LIFE_TIME_HRS = 24;

builder.Services.Configure<JwtOptions>(options =>
{
    options.Issuer = jwtOptions["Issuer"]!;
    options.Audience = jwtOptions["Audience"]!;
    options.AccessValidFor = TimeSpan.FromHours(ACCESS_TOKEN_LIFE_TIME_HRS);
    options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
});

#endregion

#region Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,

        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
        ValidAudience = builder.Configuration["JwtOptions:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"]!)),

        ClockSkew = TimeSpan.Zero // Removes default additional time to the tokens
    };
});

#endregion

#region Services Section

var connectionString = builder.Configuration.GetConnectionString("Network");
builder.Services.AddCustomRepositories(connectionString!);

#endregion

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();