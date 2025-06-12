namespace PortafolioAPI.Models.DTOs
{
    public class UpdateWorkDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
        public int IdWork { get; set; }
    }
    public class UpdateWorkDTOforRepos : UpdateWorkDTO
    {
        public int IdUser { get; set; }
    }
}
