using HumanResourcesWebApi.Models.Requests.Cities;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface ICitiesRepository
{
    Task<(List<CityDTO> cities, PageInfo pageInfo)> GetCitiesAsync(int itemsPerPage = 50, int currentPage = 1);
    Task AddCityAsync(AddCityRequest request);
    Task UpdateCityAsync(UpdateCityRequest request);
    Task DeleteCityAsync(int id);
}
