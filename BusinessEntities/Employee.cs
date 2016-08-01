using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{

    public class FirstNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please provide a first name.");
            }
            else
            {
                if (value.ToString().Contains("@"))
                {
                    return new ValidationResult("First name should NOT contain @");
                }
            }

            return ValidationResult.Success;

        }
    }
    public class Employee
    {
        [Key]

        public int EmployeeId { get; set; }

        [FirstNameValidation]
        public string FirstName { get; set;  }
        [Required(ErrorMessage ="Enter last name.")]
        [StringLength(5, ErrorMessage = "Last name length should not be greater than 5")]
        public string LastName { get; set;  }

        [Range(typeof(int),"5000","50000", ErrorMessage = "Put a proper salary value between 5,000 and 50,000")]
        public int Salary { get; set;  }


    }

    
}