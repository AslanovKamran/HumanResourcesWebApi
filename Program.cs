using HumanResourcesWebApi.Repository.Dapper;
using HumanResourcesWebApi.Abstract;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Network")!;

builder.Services.AddScoped<IOrganizationStructuresRepository, OrganizationStructuresRepository>(provider => new OrganizationStructuresRepository(connectionString));
builder.Services.AddScoped<IStateWorkTypesRepository, StateWorkTypesRepository>(provider => new StateWorkTypesRepository(connectionString));
builder.Services.AddScoped<IStateTablesRepository, StateTablesRepository>(provider => new StateTablesRepository(connectionString));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

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
