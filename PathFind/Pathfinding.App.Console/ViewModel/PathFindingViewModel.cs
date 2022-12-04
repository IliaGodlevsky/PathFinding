using Autofac.Core.Lifetime;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.App.Console.Views;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(2)]
    internal sealed class PathfindingViewModel : SafeViewModel, IDisposable
    {       
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;

        public PathfindingViewModel(IPathfindingRangeBuilder<Vertex> rangeBuilder, ILog log, IMessenger messenger)
            : base(log)
        {
            this.rangeBuilder = rangeBuilder;
            this.messenger = messenger;
        }

        [Condition(nameof(CanEnterPathfinding))]
        [MenuItem(MenuItemsNames.FindPath, 0)]
        private void FindPath()
        {
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                scope.Display<PathfindingProcessViewModel>();
            }
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.ChoosePathfindingRange, 1)]
        private void ChooseExtremeVertex()
        {
            DI.Container.Display<PathfindingRangeViewModel>();
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
            return rangeBuilder.Range.HasSourceAndTargetSet();
        }
    }
}