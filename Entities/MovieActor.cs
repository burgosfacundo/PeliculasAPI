namespace PeliculasAPI.Entities
{
    public class MovieActor
    {
        public int IdMovie { get; set; }
        public int IdActor { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
