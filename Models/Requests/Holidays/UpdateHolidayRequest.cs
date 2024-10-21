using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Holidays;

public class UpdateHolidayRequest
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Date is required")]
    [DataType(DataType.Date)] // Ensures that the input is treated as a date
    public DateTime Date { get; set; }

    [MaxLength(100, ErrorMessage = "Note can't be longer than 100 characters")]
    public string? Note { get; set; }

    [Required(ErrorMessage = "Holiday Type Id is required")]
    public int HolidayTypeId { get; set; }

    public int? HolidayForShiftId { get; set; }
}
