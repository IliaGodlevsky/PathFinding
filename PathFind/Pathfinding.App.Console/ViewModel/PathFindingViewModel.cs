using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.App.Console.Views;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(2)]
    internal sealed class PathfindingViewModel : SafeViewModel, IParentViewModel, IDisposable
    {       
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;

        public View View { get; set; }

        public IReadOnlyCollection<IViewModel> Children { get; set; }

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
            View.Display(Children.Get<PathfindingProcessViewModel>());
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.ChoosePathfindingRange, 1)]
        private void ChooseExtremeVertex()
        {
            View.Display(Children.Get<PathfindingRangeViewModel>());
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