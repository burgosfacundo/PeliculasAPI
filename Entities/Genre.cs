using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public List<MovieGenre> MoviesGenres { get; set; }
    }
}
