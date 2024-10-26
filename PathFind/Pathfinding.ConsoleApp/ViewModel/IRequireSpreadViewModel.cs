using Pathfinding.Service.Interface;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal interface IRequireSpreadViewModel
    {
        (string Name, ISpreadLevel Spread) SpreadLevel { get; set; }
    }
}
