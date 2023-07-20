using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Validations
{
    public class FileSizeValidation : ValidationAttribute
    {
        private readonly int maxWeightMB;

        public FileSizeValidation(int maxWeightMB)
        {
            this.maxWeightMB = maxWeightMB;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile formFile = value as IFormFile;
            if (value == null || formFile == null) return ValidationResult.Success;

            if (formFile.Length > maxWeightMB * 1024 * 1024)
            {
                return new ValidationResult($"The file cannot weigh more than {maxWeightMB}mb");
            }

            return ValidationResult.Success;
        }
    }
}
