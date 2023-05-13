using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console.ValueInput.ProgrammedInput
{
    internal abstract class ConsoleProgrammedInput<T> : ProgrammedInput<T>
    {
        protected override Queue<T> GenerateCommands()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            var values = GetType()
                .GetFields(flags)
                .Where(field => field.FieldType == typeof(T))
                .Select(field => (T)field.GetValue(null));
            return new(values);
        }
    }
}
