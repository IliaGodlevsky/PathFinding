using System;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class RunVisualizationModel
    {
        public TimeSpan? AlgorithmSpeed { get; set; }

        public GraphStateModel GraphState { get; set; }

        public IReadOnlyCollection<SubAlgorithmModel> Algorithms { get; set; }
    }
}
