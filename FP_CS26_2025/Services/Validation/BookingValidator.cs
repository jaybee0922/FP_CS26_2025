using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FP_CS26_2025.Services.Models;

namespace FP_CS26_2025.Services.Validation
{
    public class BookingValidator : IValidator<BookingRequestData>
    {
        public bool Validate(BookingRequestData entity, out List<string> errors)
        {
            errors = new List<string>();

            if (entity == null)
            {
                errors.Add("Booking data cannot be null.");
                return false;
            }

            // First Name: Letters Only
            if (string.IsNullOrWhiteSpace(entity.FirstName))
                errors.Add("First Name is required.");
            else if (!Regex.IsMatch(entity.FirstName, @"^[a-zA-Z\s]+$"))
                errors.Add("First Name must contain letters only.");

            // Last Name: Letters Only
            if (string.IsNullOrWhiteSpace(entity.LastName))
                errors.Add("Last Name is required.");
            else if (!Regex.IsMatch(entity.LastName, @"^[a-zA-Z\s]+$"))
                errors.Add("Last Name must contain letters only.");

            // Phone: Numbers Only and Exactly 11 Digits
            if (string.IsNullOrWhiteSpace(entity.Phone))
                errors.Add("Phone Number is required.");
            else if (!Regex.IsMatch(entity.Phone, @"^[0-9]+$"))
                errors.Add("Phone Number must contain numbers only.");
            else if (entity.Phone.Length != 11)
                errors.Add($"Phone Number must be exactly 11 digits (current: {entity.Phone.Length}).");

            // Email: Simple format check
            if (string.IsNullOrWhiteSpace(entity.Email))
                errors.Add("Email is required.");
            else if (!entity.Email.Contains("@") || !entity.Email.Contains("."))
                errors.Add("Invalid Email Address format.");

            // Room Capacity (if available in context, though strictly data validation is here)
            // Note: Contextual validation (like capacity vs room type constants) often lives in service, 
            // but we can validate the basic logical constraints here.
            
            return errors.Count == 0;
        }

        public bool ValidateCapacity(int guests, int maxCapacity, out string error)
        {
            error = string.Empty;
            if (guests > maxCapacity)
            {
                error = $"Room capacity exceeded. Max allowed: {maxCapacity}, Requested: {guests}.";
                return false;
            }
            return true;
        }
    }
}
