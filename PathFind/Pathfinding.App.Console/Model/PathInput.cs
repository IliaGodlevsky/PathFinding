using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.GraphLib.Serialization.Core.Interface;

namespace Pathfinding.App.Console.Model
{
    internal sealed class PathInput : IPathInput, IRequireStringInput
    {
        public IInput<string> StringInput { get; set; }

        public string InputLoadPath()
        {
            return StringInput.Input(Languages.InputPathMsg);
        }

        public string InputSavePath()
        {
            return StringInput.Input(Languages.InputPathMsg);
        }
    }
}
