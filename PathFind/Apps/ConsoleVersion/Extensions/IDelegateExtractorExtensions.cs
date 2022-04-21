using ConsoleVersion.Interface;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.Extensions
{
    internal static class IDelegateExtractorExtensions
    {
        public static Action<Action> ExtractFirstOrEmpty(this IDelegateExtractor<Action<Action>> extractor, MethodInfo info, object target)
        {
            return extractor.Extract(info, target).FirstOrDefault() ?? new Action<Action>(action => action.Invoke());
        }
    }
}
