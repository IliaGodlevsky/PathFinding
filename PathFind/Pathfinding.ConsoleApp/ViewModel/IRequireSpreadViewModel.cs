namespace Pathfinding.ConsoleApp.ViewModel
{
    internal interface IRequireSpreadViewModel
    {
        (string Name, int Spread) SpreadLevel { get; set; }
    }
}
