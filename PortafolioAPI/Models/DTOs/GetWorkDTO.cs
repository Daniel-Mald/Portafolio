namespace PortafolioAPI.Models.DTOs
{
    public class GetWorkDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UrlImage { get; set; } = string.Empty;
    }
}
