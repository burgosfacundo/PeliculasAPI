using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PeliculasAPI.Helpers;
using PeliculasAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities.DTOs
{
    public class MovieDTORequest : MovieDTOPatch
    {
        [FileSizeValidation(maxWeightMB: 4)]
        [FileTypeValidation(typeFile: TypeFile.Image)]
        public IFormFile Image { get; set; }

        [ModelBinder(BinderType= typeof(TypeBinder<List<int>>))]
        public List<int> IdsGenre { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorMovieDTORequest>>))]
        public List<ActorMovieDTORequest> Actors { get; set; }
    }
}
