using HumanResourcesWebApi.Repository.Dapper;
using HumanResourcesWebApi.Abstract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Network")!;

#region Services

builder.Services.AddScoped<IOrganizationStructuresRepository, OrganizationStructuresRepository>(provider => new OrganizationStructuresRepository(connectionString));
builder.Services.AddScoped<IStateWorkTypesRepository, StateWorkTypesRepository>(provider => new StateWorkTypesRepository(connectionString));
builder.Services.AddScoped<IStateTablesRepository, StateTablesRepository>(provider => new StateTablesRepository(connectionString));
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>(provider => new EmployeesRepository(connectionString));
builder.Services.AddScoped<IEducationRepository, EducationRepository>(provider => new EducationRepository(connectionString));
builder.Services.AddScoped<ICertificatesRepository, CertificatesRepositoty>(provider => new CertificatesRepositoty(connectionString));
builder.Services.AddScoped<IWorkActivitiesRepository, WorkActivitiesRepository>(provider => new WorkActivitiesRepository(connectionString));

#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{   
    c.DisplayRequestDuration();
});

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
