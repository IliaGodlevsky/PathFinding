using ConsoleVersion.Attributes;
using System;

namespace ConsoleVersion.Model.DelegateExtractors
{
    internal sealed class SafeMethodExtractor : BaseDelegateExtractor<Action<Action>, ExecuteSafeAttribute>
    {
    }
}
