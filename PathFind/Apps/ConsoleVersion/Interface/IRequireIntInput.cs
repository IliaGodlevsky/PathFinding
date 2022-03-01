using ConsoleVersion.ValueInput;

namespace ConsoleVersion.Interface
{
    internal interface IRequireIntInput
    {
        ConsoleValueInput<int> IntInput { get; set; }
    }
}
