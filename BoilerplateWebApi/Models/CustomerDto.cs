﻿namespace BoilerplateWebApi.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string? Address { get; set; }
        //public string? City { get; set; } 
        //public string? PostalCode { get; set; }
        //public string? Country { get; set; }
        public string? Phone { get; set; }
        //public string? Fax { get; set; } = string.Empty;  
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
