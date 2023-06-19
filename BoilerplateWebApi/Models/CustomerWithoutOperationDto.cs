namespace BoilerplateWebApi.Models
{
    public class CustomerWithoutOperationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; } 
        public string Email { get; set; } = string.Empty;
    }
}
