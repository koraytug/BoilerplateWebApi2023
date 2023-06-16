using System.ComponentModel.DataAnnotations;

namespace BoilerplateWebApi.Models
{
    public class CustomerOperationForCreationDto
    {
        [Required(ErrorMessage ="You should provide a name value")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public double? Price { get; set; } = 0;
    }
}
