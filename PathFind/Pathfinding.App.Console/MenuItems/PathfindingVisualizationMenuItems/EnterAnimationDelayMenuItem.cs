using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Shared.Primitives.ValueRange;
using System;

namespace Pathfinding.App.Console.MenuItems.PathfindingVisualizationMenuItems
{
    internal sealed class EnterAnimationDelayMenuItem : IMenuItem
    {
        private static readonly string Message = MessagesTexts.DelayTimeInputMsg;

        private readonly IInput<TimeSpan> spanInput;
        private readonly IMessenger messenger;

        private InclusiveValueRange<TimeSpan> DelayRange { get; }

        private bool IsVisualizationApplied { get; set; }

        public int Order => 2;

        public EnterAnimationDelayMenuItem(IInput<TimeSpan> spanInput, IMessenger messenger)
        {
            DelayRange = Constants.AlgorithmDelayTimeValueRange;
            this.spanInput = spanInput;
            this.messenger = messenger;
            this.messenger.Register<ApplyVisualizationMessage>(this, OnVisualizationApplied);
        }

        public void Execute()
        {
            using (Cursor.CleanUpAfter())
            {
                var delay = spanInput.Input(Message, DelayRange);
                messenger.Send(new AnimationDelayMessage(delay));
            }
        }

        public bool CanBeExecuted() => IsVisualizationApplied;

        private void OnVisualizationApplied(ApplyVisualizationMessage message)
        {
            IsVisualizationApplied = message.IsApplied;
        }

        public override string ToString() => "Animation delay";
    }
}
