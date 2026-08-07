using System.ComponentModel.DataAnnotations;

namespace Ecommerce531.CustomValidations
{
    public class MinMaxCustomeLengthAttribute : ValidationAttribute
    {
        private int MinLength; 
        private int MaxLength; 
        public MinMaxCustomeLengthAttribute(int minLength , int maxLength)
        {
            this.MinLength = minLength; 
            this.MaxLength = maxLength;
        }
        public override string FormatErrorMessage(string name)
        {
            return $"the field {name}  must be between {MinLength} and {MaxLength}"; 
        }
        public override bool IsValid(object? value)
        {
            if(value is string name)
            {
                if (name.Length > MinLength && name.Length <= MaxLength)
                    return true; 
            }
            return false ; 
        }
    }
}
