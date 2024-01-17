﻿using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Utils
{
    public class AllowedExtensionAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower())) 
                {
                    return new ValidationResult("This photo extension is not allowed.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
