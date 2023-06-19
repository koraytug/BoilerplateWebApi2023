using BoilerplateWebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerplateWebApi.Entities
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }
        public string? Phone { get; set; }  
        public string Email { get; set; } = string.Empty;

       
        public ICollection<CustomerOperation>? CustomerOperation { get; set; }
            = new List<CustomerOperation>();
    }
}
