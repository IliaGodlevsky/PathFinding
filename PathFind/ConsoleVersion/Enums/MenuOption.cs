using System.ComponentModel;

namespace ConsoleVersion.Enums
{

    internal enum MenuOption : byte
    {
        [Description("Quit program")]
        Quit,
        [Description("Find path")]
        PathFind,
        [Description("Save graph")]
        SaveGraph,
        [Description("Load graph")]
        LoadGraph,
        [Description("Create graph")]
        CreateGraph,
        [Description("Refresh graph")]
        RefreshGraph,
        [Description("Reverse vertex")]
        Reverse,
        [Description("Change vertex value")]
        ChangeValue
    };
}
