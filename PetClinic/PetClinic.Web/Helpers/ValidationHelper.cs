using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetClinic.Web.Helpers
{
    public class ValidationHelper
    {
        public const string EmailRegularExpression = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}";
        public const string UserNameRegularExpression = @"([a-z0-9_]{5,25})";
    }
}