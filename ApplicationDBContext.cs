using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entities;

namespace PeliculasAPI
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.IdGenre, mg.IdMovie });
            modelBuilder.Entity<MovieActor>().HasKey(ma => new { ma.IdMovie, ma.IdActor });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }
    }
}
