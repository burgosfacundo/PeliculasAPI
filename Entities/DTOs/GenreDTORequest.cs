using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities.DTOs
{
    public class GenreDTORequest
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
