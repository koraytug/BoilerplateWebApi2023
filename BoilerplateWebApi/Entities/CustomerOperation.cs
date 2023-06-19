using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BoilerplateWebApi.Entities
{
    public class CustomerOperation       
    {
        public CustomerOperation(string name)
        {
            Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } 
        public double? Price { get; set; }

        //to navigate to the customer from operation
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }
    }
}