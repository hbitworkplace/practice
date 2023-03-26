using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace CityInfoApi.Model
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage ="Provide Value For Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
