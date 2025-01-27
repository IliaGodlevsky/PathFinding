﻿using Pathfinding.App.Console.Model;
using Pathfinding.Domain.Interface;
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IGraphFieldViewModel
    {
        IGraph<GraphVertexModel> Graph { get; }

        ReactiveCommand<GraphVertexModel, Unit> ChangeVertexPolarityCommand { get; }

        ReactiveCommand<GraphVertexModel, Unit> DecreaseVertexCostCommand { get; }

        ReactiveCommand<GraphVertexModel, Unit> IncreaseVertexCostCommand { get; }

        ReactiveCommand<GraphVertexModel, Unit> ReverseVertexCommand { get; }

        ReactiveCommand<GraphVertexModel, Unit> InverseVertexCommand { get; }
    }
}