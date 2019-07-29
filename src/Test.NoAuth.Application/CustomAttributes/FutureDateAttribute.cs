using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Test.NoAuth.CustomAttributes
{
    class FutureDateAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && (DateTime)value > DateTime.Now)
                return ValidationResult.Success;
            return new ValidationResult("Invalid date");
        }
        
    }
}
