using System.ComponentModel;

namespace ConsoleVersion.Enums
{

    public enum MenuOption
    {
        Quit,
        [Description("Find path")]
        PathFind,
        Save,
        Load,
        Create,
        Refresh,
        Reverse
    };
}
