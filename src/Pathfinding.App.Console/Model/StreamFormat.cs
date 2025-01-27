using System.ComponentModel;

namespace Pathfinding.App.Console.Model;

internal enum StreamFormat
{
    [Description(".json")] Json,
    [Description(".dat")] Binary,
    [Description(".xml")] Xml
}
