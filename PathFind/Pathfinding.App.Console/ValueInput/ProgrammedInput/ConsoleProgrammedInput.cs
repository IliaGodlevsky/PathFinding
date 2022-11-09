using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console.ValueInput.ProgrammedInput
{
    internal abstract class ConsoleProgrammedInput<T> : ProgrammedInput<T>
    {
        private const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Static;

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
