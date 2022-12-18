using Pathfinding.App.Console.Interface;
using System.IO;

namespace Pathfinding.App.Console.ValueInput.RecordingInput
{
    internal abstract class RecordingInput<T> : IInput<T>
    {
        private readonly IInput<T> input;
        private readonly string path;

        protected RecordingInput(IInput<T> input, string path)
        {
            this.path = path;
            this.input = input;
        }

        public T Input()
        {
            T value = input.Input();
            Record(value);
            return value;
        }

        protected virtual void Record(T value)
        {
            using (var stream = new StreamWriter(path, append: true))
            {
                Write(stream, value);
            }
        }

        protected virtual void Write(StreamWriter writer, T value)
        {
            writer.WriteLine("Record:{0}", value);
        }
    }
}