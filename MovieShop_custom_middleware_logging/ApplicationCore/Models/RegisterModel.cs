using ApplicationCore.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Email can not be empty ")]
        [EmailAddress(ErrorMessage = "Email address is not a valid format ")]
        [StringLength(100, ErrorMessage="Email must be 100 characters or fewer")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password can not be empty")]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", 
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [YearValidationAttribute(1900,  2024, ErrorMessage = "Year must be between 1900 and 2024")]
        public DateTime DateOfBirth { get; set; }
    }
}
