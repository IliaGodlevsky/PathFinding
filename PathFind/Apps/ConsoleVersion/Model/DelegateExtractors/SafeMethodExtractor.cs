using ConsoleVersion.Attributes;
using System;

namespace ConsoleVersion.Model.DelegateExtractors
{
    internal sealed class SafeMethodExtractor : DelegateExtractor<Action<Action>, ExecuteSafeAttribute>
    {
    }
}
