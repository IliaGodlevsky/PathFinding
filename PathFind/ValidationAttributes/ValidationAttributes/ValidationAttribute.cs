using System;

namespace ValidationAttributes.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public abstract class ValidationAttribute : Attribute
    {
        public abstract void Validate(object paramter, string exceptionMessage);
    }
}
