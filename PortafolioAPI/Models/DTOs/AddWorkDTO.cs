namespace PortafolioAPI.Models.DTOs
{
    public class AddWorkDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;


    }
    public class AddWorkDTOForRepos : AddWorkDTO
    {
        public int IdUser { get; set; }
    }
}
