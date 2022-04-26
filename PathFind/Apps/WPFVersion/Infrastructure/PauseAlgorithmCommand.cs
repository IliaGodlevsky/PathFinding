﻿using Algorithm.Base;

namespace WPFVersion.Infrastructure
{
    internal sealed class PauseAlgorithmCommand : BaseAlgorithmCommand
    {
        public PauseAlgorithmCommand(PathfindingAlgorithm algorithm) : base(algorithm)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return algorithm.IsInProcess && !algorithm.IsPaused;
        }

        public override void Execute(object parameter)
        {
            algorithm.Pause();
        }
    }
}