using AutoMapper;
using PeliculasAPI.Entities;
using PeliculasAPI.Entities.DTOs;

namespace PeliculasAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDTOResponse>().ReverseMap();
            CreateMap<GenreDTORequest, Genre>();
        }
    }
}
