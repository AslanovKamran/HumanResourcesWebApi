using HumanResourcesWebApi.Repository.Dapper;
using HumanResourcesWebApi.Abstract;

namespace HumanResourcesWebApi.ServiceExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IOrganizationStructuresRepository, OrganizationStructuresRepository>(provider => new OrganizationStructuresRepository(connectionString));
        services.AddScoped<IStateWorkTypesRepository, StateWorkTypesRepository>(provider => new StateWorkTypesRepository(connectionString));
        services.AddScoped<IStateTablesRepository, StateTablesRepository>(provider => new StateTablesRepository(connectionString));
        services.AddScoped<IEmployeesRepository, EmployeesRepository>(provider => new EmployeesRepository(connectionString));
        services.AddScoped<IEducationRepository, EducationRepository>(provider => new EducationRepository(connectionString));
        services.AddScoped<ICertificatesRepository, CertificatesRepositoty>(provider => new CertificatesRepositoty(connectionString));
        services.AddScoped<IWorkActivitiesRepository, WorkActivitiesRepository>(provider => new WorkActivitiesRepository(connectionString));
        services.AddScoped<IVacationsRepository, VacationsRepository>(provider => new VacationsRepository(connectionString));
        services.AddScoped<IAwardsRepository, AwardsRepository>(provider => new AwardsRepository(connectionString));
        services.AddScoped<IReprimandsRepository, ReprimandsRepository>(provider => new ReprimandsRepository(connectionString));
        services.AddScoped<IMedalsRepository, MedalsRepository>(provider => new MedalsRepository(connectionString));
        services.AddScoped<IIdentityCardsRepository, IdentityCardsRepository>(provider => new IdentityCardsRepository(connectionString));

        return services;
    }
}
