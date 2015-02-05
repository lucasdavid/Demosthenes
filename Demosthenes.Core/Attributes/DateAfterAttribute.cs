using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TimeEqualsOrSmallerThanAttribute : ValidationAttribute
    {
        private string _property;
        
        public TimeEqualsOrSmallerThanAttribute(string property)
        {
            _property = property;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var first  = (TimeSpan)value;
            var second = (TimeSpan)validationContext.ObjectType
                .GetProperty(_property)
                .GetValue(validationContext.ObjectInstance, null);

            return first <= second
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage)
                ;
        }
    }
}
