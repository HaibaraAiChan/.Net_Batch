using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Validators
{
    public class YearValidationAttribute: ValidationAttribute
    {
        public int MinYear { get; }
        public int MaxYear { get; }
        public string ErrorMessage { get; set; }
        //public YearValidationAttribute(int minYear, int maxYear, string errorMessage )
        //{
        //    MinYear = minYear;
        //    MaxYear = maxYear;
        //    ErrorMessage = errorMessage;
        //}
        public YearValidationAttribute(int minYear, int maxYear)
        {   MinYear = minYear;
            MaxYear = maxYear;
            
        }
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var userEnterdYear=((DateTime)value).Year;
            if (value is DateTime dateValue)
            {
                int year = dateValue.Year;
                if (year < MinYear || year > MaxYear)
                {
                    //return new ValidationResult(ErrorMessage ?? $"Year must be between {MinYear} and {MaxYear}");
                    return new ValidationResult(ErrorMessage ?? $"Year must be between {MinYear} and {MaxYear}");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid date format");
        }
    }
}
