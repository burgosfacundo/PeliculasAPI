namespace PeliculasAPI.Entities
{
    public class MovieGenre
    {
        public int IdGenre { get; set; }
        public int IdMovie { get; set; }
        public Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}
