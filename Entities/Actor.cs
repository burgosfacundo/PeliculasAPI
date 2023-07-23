using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Image { get; set; }
        public List<MovieActor> MoviesActors { get; set; }
    }
}
