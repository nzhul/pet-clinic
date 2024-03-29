﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Security;

namespace PetClinic.Web.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
    {
        private const string defaultErrorMessage = "'{0}' must be at least {1} characters long.";

        private readonly int minCharacters = Membership.Provider.MinRequiredPasswordLength;

        public ValidatePasswordLengthAttribute()
            : base(defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name, minCharacters);
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;

            return (valueAsString != null && valueAsString.Length >= minCharacters);
        }
    }
}