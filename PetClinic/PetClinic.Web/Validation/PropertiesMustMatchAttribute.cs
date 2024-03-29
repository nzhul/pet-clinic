﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PetClinic.Web.Validation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        private const string defaultErrorMessage = "'{0}' and '{1}' do not match.";

        private readonly object typeId = new object();

        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(defaultErrorMessage)
        {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty
        {
            get;
            private set;
        }

        public string OriginalProperty
        {
            get;
            private set;
        }

        public override object TypeId
        {
            get
            {
                return typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentUICulture, ErrorMessageString, OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object value)
        {
            var properties = TypeDescriptor.GetProperties(value);

            object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
            object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);

            return Equals(originalValue, confirmValue);
        }
    }
}