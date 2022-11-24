using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Adapter;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Realizations.VisualizationCommands;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Executable.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Pathfinding.Visualization.Core.Abstractions
{
    public class VisualPathfindingRange<TVertex> : PathfindingRange<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> returnVerticesVisualCommands;
        private TVertex source;
        private TVertex target;

        public override TVertex Source
        {
            get => source;
            protected set
            {
                source?.VisualizeAsRegular();
                source = value;
                source?.VisualizeAsSource();
            }
        }

        public override TVertex Target 
        {
            get => target;
            protected set
            {
                target?.VisualizeAsRegular();
                target = value;
                target?.VisualizeAsTarget();
            }
        }

        protected override IList<TVertex> Intermediates { get; }

        protected override IList<TVertex> ToReplaceIntermediates { get; }

        public VisualPathfindingRange()
        {
            var intermediates = new ObservableCollection<TVertex>();
            intermediates.CollectionChanged += OnIntermediatesChanged;
            Intermediates = intermediates;
            var toReplace = new ObservableCollection<TVertex>();
            toReplace.CollectionChanged += OnMarkedToReplaceChanged;
            ToReplaceIntermediates = toReplace;
            returnVerticesVisualCommands = new RestoreVerticesVisualCommands<TVertex>(this);
        }

        public void RestoreVerticesVisualState()
        {
            var vertices = new List<TVertex>(Intermediates) { Source, Target };
            returnVerticesVisualCommands.Execute(vertices);
        }

        private void OnIntermediatesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ((TVertex)e.NewItems[0]).VisualizeAsIntermediate();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ((TVertex)e.OldItems[0]).VisualizeAsRegular();
                    break;
            }
        }

        private void OnMarkedToReplaceChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ((TVertex)e.NewItems[0]).VisualizeAsMarkedToReplaceIntermediate();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ((TVertex)e.OldItems[0]).VisualizeAsIntermediate();
                    break;
            }
        }
    }
}