using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities.DTOs
{
    public class MovieDTOPatch
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        public bool OnBillboard { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
