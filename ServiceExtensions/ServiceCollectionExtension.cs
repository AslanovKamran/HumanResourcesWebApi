using HumanResourcesWebApi.Repository.Dapper;
using HumanResourcesWebApi.Abstract;

namespace HumanResourcesWebApi.ServiceExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IOrganizationStructuresRepository, OrganizationStructuresRepository>(provider => new OrganizationStructuresRepository(connectionString));
        services.AddScoped<IGeneralInformationRepository, GeneralInformationRepository>(provider => new GeneralInformationRepository(connectionString));
        services.AddScoped<IWorkActivitiesRepository, WorkActivitiesRepository>(provider => new WorkActivitiesRepository(connectionString));
        services.AddScoped<IIdentityCardsRepository, IdentityCardsRepository>(provider => new IdentityCardsRepository(connectionString));
        services.AddScoped<IFamilyMembersRepository, FamilyMembersRepository>(provider => new FamilyMembersRepository(connectionString));
        services.AddScoped<IPreviousNamesRepository, PreviousNamesRepository>(provider => new PreviousNamesRepository(connectionString));
        services.AddScoped<IVacationTypesRepository, VacationTypesRepository>(provider => new VacationTypesRepository(connectionString));
        services.AddScoped<ICertificatesRepository, CertificatesRepositoty>(provider => new CertificatesRepositoty(connectionString));
        services.AddScoped<IStateTablesRepository, StateTablesRepository>(provider => new StateTablesRepository(connectionString));
        services.AddScoped<IReprimandsRepository, ReprimandsRepository>(provider => new ReprimandsRepository(connectionString));
        services.AddScoped<IEmployeesRepository, EmployeesRepository>(provider => new EmployeesRepository(connectionString));
        services.AddScoped<IEducationRepository, EducationRepository>(provider => new EducationRepository(connectionString));
        services.AddScoped<IVacationsRepository, VacationsRepository>(provider => new VacationsRepository(connectionString));
        services.AddScoped<ICountriesRepository, CountriesRepository>(provider => new CountriesRepository(connectionString));
        services.AddScoped<IWorkNormsRepository, WorkNormsRepository>(provider => new WorkNormsRepository(connectionString));
        services.AddScoped<IHolidaysRepository, HolidaysRepository>(provider => new HolidaysRepository(connectionString));
        services.AddScoped<IAwardsRepository, AwardsRepository>(provider => new AwardsRepository(connectionString));
        services.AddScoped<IMedalsRepository, MedalsRepository>(provider => new MedalsRepository(connectionString));
        services.AddScoped<ICitiesRepository, CitiesRepository>(provider => new CitiesRepository(connectionString));

        return services;
    }
}
