using ConsoleVersion.Attributes;
using System;

namespace ConsoleVersion.Model.DelegateExtractors
{
    internal sealed class ValidationMethodExtractor : DelegateExtractor<Func<bool>, PreValidationMethodAttribute>
    {
    }
}
