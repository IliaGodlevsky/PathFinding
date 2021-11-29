using ConsoleVersion.ValueInput;

namespace ConsoleVersion.Interface
{
    internal interface IRequireInt32Input
    {
        ConsoleValueInput<int> Int32Input { get; set; }
    }
}
