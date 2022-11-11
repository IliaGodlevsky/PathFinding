using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Collections;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class PathfindingProcessSelectingViewModel : ViewModel, IRequireIntInput
    {
        private readonly IMessenger messenger;
        private readonly InclusiveValueRange<int> algorithmKeysValueRange;
        private readonly IPathfindingRange range;

        public IInput<int> IntInput { get; set; }

        public string AlgorithmKeyInputMessage { private get; set; }

        private int AlgorithmIndex => IntInput.Input(AlgorithmKeyInputMessage, algorithmKeysValueRange) - 1;

        private ReadOnlyList<IAlgorithmFactory<PathfindingProcess>> Factories { get; }

        public PathfindingProcessSelectingViewModel(IEnumerable<IAlgorithmFactory<PathfindingProcess>> factories,
            IMessenger messenger, IPathfindingRange range, ILog log) 
            : base(log)
        {
            Factories = factories.ToReadOnly();
            this.messenger = messenger;
            this.range = range;
            algorithmKeysValueRange = new InclusiveValueRange<int>(Factories.Count, 1);
        }

        [Order(0)]
        [MenuItem("Create pathfinding algorithm")]
        private void CreatePathfindingProcess()
        {
            var algorithm = Factories[AlgorithmIndex];
            //messenger.Send(new PathfindingAlgorithmCreated(algorithm));
        }
    }
}