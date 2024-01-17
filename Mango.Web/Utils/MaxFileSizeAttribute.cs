﻿using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Utils
{
    public class MaxFileSize : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSize(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                if (file.Length > (_maxFileSize *1-024 * 1024))
                {
                    return new ValidationResult($"Maximum allowed file size is {_maxFileSize} MB.");
                }
            }
            return ValidationResult.Success;
        }
    }

}
