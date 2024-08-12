using Pathfinding.Domain.Interface.Factories;
using ReactiveUI;
using Shared.Primitives.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.ViewModel
{
    public class FactoriesChooseViewModel : ReactiveObject
    {
        public IReadOnlyCollection<(string Name, INeighborhoodFactory Factory)> Factories { get; }

        public IReadOnlyCollection<(string Name, IGraphAssemble<VertexViewModel> Assembler)> Assemblers { get; }

        public IReadOnlyCollection<(string Name, ReturnOptions Option)> CycleOptions { get; }

        public INeighborhoodFactory Factory { get; set; }

        public IGraphAssemble<VertexViewModel> Assembler { get; set; }

        public ReturnOptions ReturnOption { get; set; }

        public FactoriesChooseViewModel(IReadOnlyCollection<(string Name, INeighborhoodFactory Factory)> factories,
            IReadOnlyCollection<(string Name, IGraphAssemble<VertexViewModel> Assembler)> assemblers)
        {
            Factories = factories;
            Assemblers = assemblers;
            CycleOptions = new List<(string Name, ReturnOptions Option)>()
            {
                ("Cycle", ReturnOptions.Cycle ),
                ("Limit", ReturnOptions.Limit)
            }.AsReadOnly();
        }
    }
}
