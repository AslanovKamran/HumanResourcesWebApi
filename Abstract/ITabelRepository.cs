using HumanResourcesWebApi.Models.Domain.TabelModels;

namespace HumanResourcesWebApi.Abstract;

public interface ITabelRepository
{
    Task<IEnumerable<TabelModel>> GetTabelDataAsync(int year, int month, int? organizationStructureId);
}
