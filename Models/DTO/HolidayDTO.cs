using HumanResourcesWebApi.Models.Domain;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace HumanResourcesWebApi.Models.DTO
{
    public class HolidayDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Shift { get; set; }
        public string? Note{ get; set; }

    }

    
}
