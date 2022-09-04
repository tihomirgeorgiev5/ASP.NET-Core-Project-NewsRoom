using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NewsRoom.Data
{
    public class DateValidator : ValidationAttribute
    {
        private const string DateTimeFormat = "dd-MM-yyyy";
        private readonly DateTime date;

        public DateValidator(string dateInput)
        {
            this.date = DateTime.ParseExact(dateInput, DateTimeFormat, CultureInfo.InvariantCulture);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value >= this.date) return new ValidationResult(this.ErrorMessage);
            return ValidationResult.Success;
        }
    }
}
