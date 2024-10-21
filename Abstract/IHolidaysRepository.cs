using HumanResourcesWebApi.Models.Requests.Holidays;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IHolidaysRepository
{
    public Task<List<HolidayDTO>> GetHolidaysAsync(int? year);
    public Task DeleteHolidayAsync(int id);
    public Task AddHolidayAsync(AddHolidayRequest request);
    public Task UpdateHolidayAsync(UpdateHolidayRequest request);
}
