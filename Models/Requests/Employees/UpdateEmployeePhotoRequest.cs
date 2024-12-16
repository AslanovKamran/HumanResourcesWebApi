using System.Text.Json.Serialization;

namespace HumanResourcesWebApi.Models.Requests.Employees;

public class UpdateEmployeePhotoRequest
{
    public int Id { get; set; }
    public string PhotoUrl { get; set; } = string.Empty;

    [JsonIgnore]
    public IFormFile? ImageFile { get; set; }
}
