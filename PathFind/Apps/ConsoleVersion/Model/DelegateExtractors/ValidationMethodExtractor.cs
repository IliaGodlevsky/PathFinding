using ConsoleVersion.Attributes;
using System;

namespace ConsoleVersion.Model.DelegateExtractors
{
    internal sealed class ValidationMethodExtractor : BaseDelegateExtractor<Func<bool>, PreValidationMethodAttribute>
    {
    }
}
