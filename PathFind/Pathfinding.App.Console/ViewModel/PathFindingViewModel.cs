using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.App.Console.Views;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(2)]
    internal sealed class PathfindingViewModel : SafeViewModel, IRequireAnswerInput, IDisposable
    {       
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeAdapter<Vertex> adapter;

        public IInput<Answer> AnswerInput { get; set; }

        public PathfindingViewModel(IPathfindingRangeAdapter<Vertex> adapter, ILog log, IMessenger messenger)
            : base(log)
        {
            this.adapter = adapter;
            this.messenger = messenger;
        }

        [Condition(nameof(CanEnterPathfinding))]
        [MenuItem(MenuItemsNames.FindPath, 0)]
        private void FindPath()
        {
            DI.Container.DisplayScoped<PathfindingProcessView>();
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.ChoosePathfindingRange, 1)]
        private void ChooseExtremeVertex()
        {
            DI.Container.Display<PathfindingRangeView>();
        }

        [MenuItem(MenuItemsNames.ClearGraph, 2)]
        private void ClearGraph()
        {
            using (Cursor.HideCursor())
            {
                messenger.Send(new ClearGraphMessage());
            }
        }

        [FailMessage(MessagesTexts.NoPathfindingRangeMsg)]
        private bool CanEnterPathfinding()
        {
            return adapter.HasSourceAndTargetSet();
        }
    }
}