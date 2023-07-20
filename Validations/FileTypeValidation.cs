using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Validations
{
    public class FileTypeValidation : ValidationAttribute
    {
        private readonly string[] validType;

        public FileTypeValidation(string[] validType)
        {
            this.validType = validType;
        }
        public FileTypeValidation(TypeFile typeFile)
        {
            if (typeFile == TypeFile.Image)
            {
                this.validType = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile formFile = value as IFormFile;
            if (value == null || formFile == null) return ValidationResult.Success;

            if(!validType.Contains(formFile.ContentType))
            {
                return new ValidationResult($"The type of the file must be: {string.Join(",", validType)}");
            }

            return ValidationResult.Success;
        }
    }
}
