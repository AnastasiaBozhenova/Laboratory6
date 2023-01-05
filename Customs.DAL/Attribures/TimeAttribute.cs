using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Customs.DAL.Attribures
{
    public class TimeAttribute : ValidationAttribute
    {
        private const byte MaxHours = 23;
        private const byte MinHours = 0;

        private const byte MaxMinutes = 59;
        private const byte MinMinutes = 0;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringValue = (string) value;
            var times = stringValue.Split(":")
                .Select(int.Parse)
                .ToList();

            var minutes = times[1];
            var hours = times[0];

            return minutes is >= MinMinutes and <= MaxMinutes && hours is >= MinHours and <= MaxHours
                ? ValidationResult.Success
                : new ValidationResult($"Время должно быть больше {MinHours}:{MinMinutes} и меньше {MaxHours}:{MaxMinutes}");
        }
    }
}