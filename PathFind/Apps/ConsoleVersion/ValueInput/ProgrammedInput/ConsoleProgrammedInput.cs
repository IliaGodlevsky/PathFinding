using Common.Extensions.EnumerableExtensions;
using System.Collections.Generic;
using System.Linq;

using static System.Reflection.BindingFlags;

namespace ConsoleVersion.ValueInput.ProgrammedInput
{
    internal abstract class ConsoleProgrammedInput<T> : ProgrammedInput<T>
    {
        protected override Queue<T> GenerateCommands()
        {
            return GetType()
                .GetFields(NonPublic | Static)
                .Where(field => field.FieldType == typeof(T))
                .Select(field => (T)field.GetValue(null))
                .ToQueue();
        }
    }
}
