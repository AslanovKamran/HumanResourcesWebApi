using HumanResourcesWebApi.Models.Requests.Countries;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface ICountriesRepository
{
    Task<List<Country>> GetCountriesAsync();
    Task AddCountryAsync (AddCountryRequest request);
    Task UpdateCountryAsync (UpdateCountryRequest request);
    Task DeleteCountryAsync (int id);

}
