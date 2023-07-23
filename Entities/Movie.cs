using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required] [StringLength(200)]
        public string Title { get; set; }
        public bool OnBillboard { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Image { get; set; }
        public List<MovieGenre> MoviesGenres { get; set; }
        public List<MovieActor> MoviesActors { get; set; }
    }
}
