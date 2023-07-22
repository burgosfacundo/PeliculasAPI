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
            CreateMap<ActorDTORequest, Actor>().ForMember(a => a.Image, options => options.Ignore());
            CreateMap<Actor,ActorDTOPatch>().ReverseMap();

            CreateMap<Movie, MovieDTOResponse>();
            CreateMap<MovieDTORequest, Movie>().ForMember(m => m.Image, options => options.Ignore());
            CreateMap<Movie, MovieDTOPatch>().ReverseMap();


        }
    }
}
