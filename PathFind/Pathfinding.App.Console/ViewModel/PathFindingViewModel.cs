using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.MenuCommands.Attributes;
using Pathfinding.App.Console.Views;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(2)]
    internal sealed class PathfindingViewModel : SafeViewModel, IRequireAnswerInput, IDisposable
    {       
        private readonly IMessenger messenger;
        private readonly IPathfindingRange range;

        public IInput<Answer> AnswerInput { get; set; }

        public PathfindingViewModel(IPathfindingRange range, ILog log, IMessenger messenger)
            : base(log)
        {
            this.range = range;
            this.messenger = messenger;
        }

        [Condition(nameof(CanEnterPathfinding))]
        [MenuItem(MenuItemsNames.FindPath, 0)]
        private void FindPath()
        {
            DI.Container.Display<PathfindingProcessView>();
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.ChooseEndPoints, 1)]
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

        [FailMessage(MessagesTexts.NoPathfindingRange)]
        private bool CanEnterPathfinding()
        {
            return range.HasSourceAndTargetSet();
        }
    }
}