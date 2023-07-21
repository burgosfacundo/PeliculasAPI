using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities.DTOs
{
    public class ActorDTOPatch
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
