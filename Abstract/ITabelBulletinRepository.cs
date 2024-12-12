using HumanResourcesWebApi.Models.Requests.TabelBulletin;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface ITabelBulletinRepository
{
    Task<List<TabelBulletinDTO>> GetTabelBulletinsAsync(int employeeId, int? beginYear, int? endYear);
    Task AddTabelBulletinAsync(AddTabelBulletinRequest request);
    Task UpdateTabelBulletinAsync(UpdateTabelBulletinRequest request);
    Task DeleteTabelBulletinByIdAsync(int id);
}
