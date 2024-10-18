using HumanResourcesWebApi.Models.Requests.Certificates;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface ICertificatesRepository
{
    Task<List<Certificate>> GetEmployeesCertificates(int employeeId);
    Task AddCertificateAsync(AddCertificateRequest request);
    Task UpdateCertificateAsync(UpdateCertificateRequest request);
    Task DeleteCertificateAsync(int id);
}
