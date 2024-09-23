namespace HumanResourcesWebApi.Models.Domain
{
    public class OrganizationStructure
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public int? ParentId { get; set; }
        public string? FirstNumber { get; set; }
        public string? SecondNumber { get; set; }
        public int? TabelOrganizationId { get; set; }
        public int? TabelPriority { get; set; }
        public bool Canceled { get; set; }
        public string? HeadName { get; set; }
        public string? HeadPosition { get; set; }
        public bool IsSeacCoef { get; set; }

    }
}