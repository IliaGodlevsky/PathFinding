using System.ComponentModel;

namespace ConsoleVersion.Enums
{
    /// <summary>
    /// Enums for creating a console menu
    /// </summary>
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

        [Description("Change vertex cost")]
        ChangeCost,

        [Description("Make weighted")]
        MakeWeighted,

        [Description("Make unweighted")]
        MakeUnweigted
    }
}
