namespace BoilerplateWebApi.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string? Address { get; set; }
        public string? Phone { get; set; }  
        public string Email { get; set; } = string.Empty;

        public int NumberOfOperations
        {
            get
            {
                return CustomerOperations.Count;
            }
        }

        public ICollection<CustomerOperationsDto> CustomerOperations { get; set; } 
            = new List<CustomerOperationsDto>(); 

    }
}
