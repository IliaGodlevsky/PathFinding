using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using GraphLib.Serialization.Interfaces;

namespace ConsoleVersion.Model
{
    internal sealed class PathInput : IPathInput, IRequireStringInput
    {
        public IInput<string> StringInput { get; set; }

        public string InputLoadPath()
        {
            return StringInput.Input(MessagesTexts.InputPathMsg);
        }

        public string InputSavePath()
        {
            return StringInput.Input(MessagesTexts.InputPathMsg);
        }
    }
}
