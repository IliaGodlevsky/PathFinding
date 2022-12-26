using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.GraphLib.Serialization.Core.Interface;

namespace Pathfinding.App.Console.Model
{
    internal sealed class PathInput : IPathInput
    {
        private readonly IInput<string> input;

        public PathInput(IInput<string> input)
        {
            this.input = input;
        }

        public string InputLoadPath()
        {
            return Input();
        }

        public string InputSavePath()
        {
            return Input();
        }

        private string Input()
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
