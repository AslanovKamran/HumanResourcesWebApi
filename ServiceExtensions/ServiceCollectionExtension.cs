using HumanResourcesWebApi.Repository.Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Tokens;

namespace HumanResourcesWebApi.ServiceExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomRepositories(this IServiceCollection services, string connectionString)
    {

        services.AddSingleton<ITokenGenerator, TokenGenerator>();

        services.AddScoped<IOrganizationStructuresRepository, OrganizationStructuresRepository>(provider => new OrganizationStructuresRepository(connectionString));
        services.AddScoped<IPreviousWorkingPlacesRepository, PreviousWorkingPlacesRepository>(provider => new PreviousWorkingPlacesRepository(connectionString));
        services.AddScoped<IBrigadeReplacementsRepository, BrigadeReplacementsRepository>(provider => new BrigadeReplacementsRepository(connectionString));
        services.AddScoped<IGeneralInformationRepository, GeneralInformationRepository>(provider => new GeneralInformationRepository(connectionString));
        services.AddScoped<IWorkActivitiesRepository, WorkActivitiesRepository>(provider => new WorkActivitiesRepository(connectionString));
        services.AddScoped<IIdentityCardsRepository, IdentityCardsRepository>(provider => new IdentityCardsRepository(connectionString));
        services.AddScoped<IFamilyMembersRepository, FamilyMembersRepository>(provider => new FamilyMembersRepository(connectionString));
        services.AddScoped<IPreviousNamesRepository, PreviousNamesRepository>(provider => new PreviousNamesRepository(connectionString));
        services.AddScoped<IVacationTypesRepository, VacationTypesRepository>(provider => new VacationTypesRepository(connectionString));
        services.AddScoped<IBusinessTripsRepository, BusinessTripsRepository>(provider => new BusinessTripsRepository(connectionString));
        services.AddScoped<ICertificatesRepository, CertificatesRepositoty>(provider => new CertificatesRepositoty(connectionString));
        services.AddScoped<IStateTablesRepository, StateTablesRepository>(provider => new StateTablesRepository(connectionString));
        services.AddScoped<ISubstitutesRepository, SubstitutesRepository>(provider => new SubstitutesRepository(connectionString));
        services.AddScoped<IReprimandsRepository, ReprimandsRepository>(provider => new ReprimandsRepository(connectionString));
        services.AddScoped<IEmployeesRepository, EmployeesRepository>(provider => new EmployeesRepository(connectionString));
        services.AddScoped<IEducationRepository, EducationRepository>(provider => new EducationRepository(connectionString));
        services.AddScoped<IVacationsRepository, VacationsRepository>(provider => new VacationsRepository(connectionString));
        services.AddScoped<ICountriesRepository, CountriesRepository>(provider => new CountriesRepository(connectionString));
        services.AddScoped<IWorkNormsRepository, WorkNormsRepository>(provider => new WorkNormsRepository(connectionString));
        services.AddScoped<IHolidaysRepository, HolidaysRepository>(provider => new HolidaysRepository(connectionString));
        services.AddScoped<IBrigadesRepository, BrigadesRepository>(provider => new BrigadesRepository(connectionString));
        services.AddScoped<IAwardsRepository, AwardsRepository>(provider => new AwardsRepository(connectionString));
        services.AddScoped<IMedalsRepository, MedalsRepository>(provider => new MedalsRepository(connectionString));
        services.AddScoped<IRightsRepository, RightsRepository>(provider => new RightsRepository(connectionString));
        services.AddScoped<ICitiesRepository, CitiesRepository>(provider => new CitiesRepository(connectionString));
        services.AddScoped<IRolesRepository, RolesRepository>(provider => new RolesRepository(connectionString));
        services.AddScoped<IUserRepository, UserRepository>(provider => new UserRepository(connectionString));

        services.AddScoped<ITabelRepository, TabelRepository>(provider => new TabelRepository(connectionString));

        services.AddScoped<IAnvizEmployeesRepository, AnvizEmployeesRepository>(provider => new AnvizEmployeesRepository(connectionString));

        services.AddScoped<ITabelVacationRepository, TabelVacationRepository>(provider => new TabelVacationRepository(connectionString));
        services.AddScoped<ITabelAbsentRepository, TabelAbsentRepository>(provider => new TabelAbsentRepository(connectionString));
        services.AddScoped<ITabelBulletinRepository, TabelBulletinRepository>(provider => new TabelBulletinRepository(connectionString));
        services.AddScoped<ITabelExtraWorkRepository, TabelExtraWorkRepository>(provider => new TabelExtraWorkRepository(connectionString));



        return services;
    }
}
