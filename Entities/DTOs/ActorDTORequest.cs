using PeliculasAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities.DTOs
{
    public class ActorDTORequest
    {
        [Required][StringLength(120)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        [FileSizeValidation(maxWeightMB:4)] [FileTypeValidation(typeFile: TypeFile.Image)]
        public IFormFile Image { get; set; }
    }
}
