using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Shared.Primitives.ValueRange;
using System;

namespace Pathfinding.App.Console.MenuItems.PathfindingVisualizationMenuItems
{
    [HighPriority]
    internal sealed class EnterAnimationDelayMenuItem : IConditionedMenuItem, ICanReceiveMessage
    {
        private readonly IInput<TimeSpan> spanInput;
        private readonly IMessenger messenger;

        private InclusiveValueRange<TimeSpan> DelayRange { get; }

        private bool IsVisualizationApplied { get; set; } = true;

        public EnterAnimationDelayMenuItem(IInput<TimeSpan> spanInput, IMessenger messenger)
        {
            DelayRange = Constants.AlgorithmDelayTimeValueRange;
            this.spanInput = spanInput;
            this.messenger = messenger;
        }

        public void Execute()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                var delay = spanInput.Input(Languages.DelayTimeInputMsg, DelayRange);
                var msg = new AlgorithmDelayMessage(delay);
                messenger.SendMany(msg, Tokens.Visualization);
            }
        }

        public bool CanBeExecuted() => IsVisualizationApplied;

        private void SetApplied(IsAppliedMessage msg)
        {
            IsVisualizationApplied = msg.IsApplied;
        }

        public override string ToString()
        {
            return Languages.EnterAnimationDelay;
        }

        public void RegisterHandlers(IMessenger messenger)
        {
            messenger.Register<EnterAnimationDelayMenuItem, IsAppliedMessage>(this, Tokens.Visualization, SetApplied);
        }
    }
}
