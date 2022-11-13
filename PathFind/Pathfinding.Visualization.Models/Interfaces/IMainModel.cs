﻿using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.Visualization.Models.Interfaces
{
    public interface IMainModel<TGraph, TVertex, TField>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex, IVisualizable
        where TField : IGraphField<TVertex>
    {
        string GraphParametres { get; set; }

        TField GraphField { get; set; }

        TGraph Graph { get; }

        void SaveGraph();

        void ConnectNewGraph(TGraph graph);

        void CreateNewGraph();

        void ClearGraph();

        void ClearColors();

        void LoadGraph();

        void FindPath();
    }
}