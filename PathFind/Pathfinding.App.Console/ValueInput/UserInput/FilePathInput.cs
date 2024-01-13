using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal sealed class FilePathInput : IFilePathInput
    {
        private readonly IInput<string> input;

        public FilePathInput(IInput<string> input)
        {
            this.input = input;
        }

        public string Input()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                string path = input.Input(Languages.InputPathMsg);
                while (string.IsNullOrEmpty(path))
                {
                    path = input.Input(Languages.InputPathMsg);
                }
                return path;
            }
        }
    }
}
