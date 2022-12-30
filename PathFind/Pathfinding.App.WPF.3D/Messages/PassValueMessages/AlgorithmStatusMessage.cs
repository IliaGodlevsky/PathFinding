using Pathfinding.App.WPF._3D.ViewModel;
using System;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class AlgorithmStatusMessage
    {
        public static AlgorithmStatusMessage Paused(Guid id)
        {
            return new AlgorithmStatusMessage(AlgorithmViewModel.Paused, id);
        }

        public static AlgorithmStatusMessage Interrupted(Guid id)
        {
            return new AlgorithmStatusMessage(AlgorithmViewModel.Interrupted, id);
        }

        public static AlgorithmStatusMessage Started(Guid id)
        {
            return new AlgorithmStatusMessage(AlgorithmViewModel.Started, id);
        }

        public string Status { get; }

        public Guid Id { get; }

        public AlgorithmStatusMessage(string statuses, Guid id)
        {
            Status = statuses;
            Id = id;
        }
    }
}
