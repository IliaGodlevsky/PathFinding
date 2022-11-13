﻿namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IVisualizable
    {
        bool IsVisualizedAsPath();

        bool IsVisualizedAsEndPoint();

        void VisualizeAsTarget();

        void VisualizeAsRegular();

        void VisualizeAsObstacle();

        void VisualizeAsPath();

        void VisualizeAsSource();

        void VisualizeAsVisited();

        void VisualizeAsEnqueued();

        void VisualizeAsIntermediate();

        void VisualizeAsMarkedToReplaceIntermediate();
    }
}