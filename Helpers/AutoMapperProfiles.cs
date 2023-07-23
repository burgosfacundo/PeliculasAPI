using AutoMapper;
using PeliculasAPI.Entities;
using PeliculasAPI.Entities.DTOs;

namespace PeliculasAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDTOResponse>();
            CreateMap<GenreDTORequest, Genre>();

            CreateMap<Actor, ActorDTOResponse>();
            CreateMap<ActorDTORequest, Actor>()
                .ForMember(a => a.Image, options => options.Ignore());
            CreateMap<Actor,ActorDTOPatch>()
                .ReverseMap();

            CreateMap<Movie, MovieDTOResponse>();
            CreateMap<MovieDTORequest, Movie>()
                .ForMember(m => m.Image, options => options.Ignore())
                .ForMember(m => m.MoviesGenres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(m => m.MoviesActors, options => options.MapFrom(MapMoviesActors));

            CreateMap<Movie, MovieDTOPatch>()
                .ReverseMap();
        }

        private List<MovieActor> MapMoviesActors(MovieDTORequest dto,Movie movie)
        {
            var result = new List<MovieActor>();
            if (dto.Actors == null)
            {
                return result;
            }

            foreach (var actor in dto.Actors)
            {
                result.Add(new MovieActor() { IdActor = actor.IdActor,Character = actor.Character });
            }

            return result;
        }

        private List<MovieGenre> MapMoviesGenres(MovieDTORequest dto,Movie movie)
        {
            var result = new List<MovieGenre>();
            if (dto.IdsGenre == null)
            {
                return result;
            }

            foreach (var id in dto.IdsGenre)
            {
                result.Add(new MovieGenre() { IdGenre = id});
            }

            return result;
        }

    }
}
