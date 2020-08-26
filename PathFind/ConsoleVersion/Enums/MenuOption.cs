using System.ComponentModel;

namespace ConsoleVersion.Enums
{

    public enum MenuOption
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
        Reverse
    };
}
