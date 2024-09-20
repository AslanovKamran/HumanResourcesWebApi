namespace HumanResourcesWebApi.Models.DTO
{
    public class OrganizationStructureListDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public int? ParentId { get; set; }
        public bool Canceled { get; set; }
        public List<OrganizationStructureListDTO> Children { get; set; } = new List<OrganizationStructureListDTO>();
    }
}
