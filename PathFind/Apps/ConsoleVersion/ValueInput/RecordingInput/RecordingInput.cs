using ConsoleVersion.Interface;
using System.Collections.Generic;
using System.IO;

namespace ConsoleVersion.ValueInput.RecordingInput
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
                stream.WriteLine("Record: {0}", value);
            }
        }
    }
}