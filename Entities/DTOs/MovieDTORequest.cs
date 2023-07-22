using PeliculasAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities.DTOs
{
    public class MovieDTORequest : MovieDTOPatch
    {
        [FileSizeValidation(maxWeightMB: 4)]
        [FileTypeValidation(typeFile: TypeFile.Image)]
        public IFormFile Image { get; set; }
    }
}
