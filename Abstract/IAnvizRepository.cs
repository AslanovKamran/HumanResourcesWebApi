using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IAnvizRepository
{
    Task<List<Anviz>> GetCheckDateAsync(DateTime inTime, DateTime endTime); 
}
