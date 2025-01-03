﻿namespace HumanResourcesWebApi.Models.DTO;

public class SubstituteByVacationDTO
{
    public int Id { get; set; }
    public int WhoId { get; set; }
    public string WhoName { get; set; } = string.Empty;
    public int WhomId { get; set; }
    public string WhomName { get; set; } = string.Empty;
    public int TabelVacationId { get; set; }
    public DateTime? SubStartDate{ get; set; }
    public DateTime? SubEndDate{ get; set; }
}
