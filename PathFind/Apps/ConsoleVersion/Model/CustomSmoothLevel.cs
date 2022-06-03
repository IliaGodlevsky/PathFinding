using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace ConsoleVersion.Model
{
    [Description("Custom")]
    internal sealed class CustomSmoothLevel : ISmoothLevel, IRequireIntInput
    {
        private const string LevelMsg = "Input level of smoothing: ";

        public IInput<int> IntInput { get; set; }

        public int Level => IntInput.Input(LevelMsg, 100);
    }
}
