using System.Collections.Generic;

namespace Pathfinding.App.Console.Interface
{
    internal interface IParentViewModel : IViewModel
    {
        IReadOnlyCollection<IViewModel> Children { get; set; }
    }
}
