using System.ComponentModel;

namespace Pathfinding.ConsoleApp.Model
{
    internal enum StreamFormat 
    {
        [Description(".json")]  Json,
        [Description(".dat")]   Binary,
        [Description(".xml")]   Xml 
    }
}
