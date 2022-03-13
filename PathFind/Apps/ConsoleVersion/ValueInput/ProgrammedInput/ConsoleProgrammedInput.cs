using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.ValueInput.ProgrammedInput
{
    internal abstract class ConsoleProgrammedInput<T> : ProgrammedInput<T>
        where T : IComparable
    {
        private const BindingFlags Flags
            = BindingFlags.NonPublic
            | BindingFlags.Instance
            | BindingFlags.Static;

        protected override Queue<T> GenerateCommands()
        {
            return GetType()
                .GetFields(Flags)
                .Where(field => field.FieldType == typeof(T))
                .Select(field => (T)field.GetValue(null))
                .ToQueue();
        }
    }
}
