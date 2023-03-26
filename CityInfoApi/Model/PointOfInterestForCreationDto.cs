using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CityInfoApi.Model
{
    public class PointOfInterestForCreationDto
    {
        [Required (ErrorMessage ="You Should Provide Value for Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string?  Description { get; set; }
    }
}
