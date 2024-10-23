using HumanResourcesWebApi.Models.Requests.GeneralInformation;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IGeneralInformationRepository
{
    Task<List<GeneralInformation>> GetGeneralInformationAsync ();
    Task UpdateGeneralInformationAsync(UpdateGeneralInformationRequest request);
}
