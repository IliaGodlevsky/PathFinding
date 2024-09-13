using Pathfinding.Shared.Primitives;
using ReactiveUI;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class RunVertexModel : ReactiveObject
    {
        private int cost;
        public int Cost
        {
            get => cost;
            set => this.RaiseAndSetIfChanged(ref cost, value);
        }

        private bool isObstacle;
        public bool IsObstacle
        {
            get => isObstacle;
            set => this.RaiseAndSetIfChanged(ref isObstacle, value);
        }

        public Coordinate Position { get; set; }

        private bool isVisited;
        public bool IsVisited
        {
            get => isVisited;
            set
            {
                if (!IsRange() && !IsPathVertex())
                {
                    if (IsEnqueued)
                    {
                        IsEnqueued = false;
                    }
                    this.RaiseAndSetIfChanged(ref isVisited, value);
                }
            }
        }

        private bool isEnqueued;
        public bool IsEnqueued
        {
            get => isEnqueued;
            set
            {
                if (!IsRange() && !IsPathVertex())
                {
                    if (IsVisited)
                    {
                        IsVisited = false;
                    }
                    this.RaiseAndSetIfChanged(ref isEnqueued, value);
                }
            }
        }

        private bool isPath;
        public bool IsPath
        {
            get => isPath;
            set
            {
                if (!IsRange())
                {
                    if (!isPath)
                    {
                        this.RaiseAndSetIfChanged(ref isPath, value);
                    }
                    else
                    {
                        IsCrossedPath = true;
                    }
                }
            }
        }

        private bool isCrossedPath;
        public bool IsCrossedPath
        {
            get => isCrossedPath;
            set => this.RaiseAndSetIfChanged(ref isCrossedPath, value);
        }

        private bool isSource;
        public bool IsSource
        {
            get => isSource;
            set => this.RaiseAndSetIfChanged(ref isSource, value);
        }

        private bool isTarget;
        public bool IsTarget
        {
            get => isTarget;
            set => this.RaiseAndSetIfChanged(ref isTarget, value);
        }

        private bool isTransit;
        public bool IsTransit
        {
            get => isTransit;
            set => this.RaiseAndSetIfChanged(ref isTransit, value);
        }

        private bool IsRange()
        {
            return IsSource || IsTarget || IsTransit;
        }

        private bool IsPathVertex()
        {
            return IsPath || IsCrossedPath;
        }
    }
}
