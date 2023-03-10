﻿using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Shared.Extensions;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingProcessUnit : Unit, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private readonly IInput<ConsoleKey> input;
        private readonly ILog log;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public PathfindingProcessUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IInput<ConsoleKey> input, IMessenger messenger, ILog log)
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
            this.log = log;
            this.rangeBuilder = rangeBuilder;
            this.input = input;
        }

        private void FindPath(DataMessage<IAlgorithmFactory<PathfindingProcess>> message)
        {
            var factory = message.Value;
            var range = rangeBuilder.Range;
            using var algorithm = factory.Create(range);
            using (Disposable.Use(ClearColors))
            {
                using (Cursor.HideCursor())
                {
                    try
                    {
                        FindPath(algorithm);
                    }
                    catch (PathfindingException ex)
                    {
                        log.Warn(ex.Message);
                    }
                }
            }
        }

        private void ClearColors()
        {
            graph.ForEach(v => v.RestoreDefaultVisualState());
            rangeBuilder.Range.RestoreVerticesVisualState();
            messenger.SendData(string.Empty, Tokens.Screen);
        }

        private void FindPath(PathfindingProcess algorithm)
        {
            var path = NullGraphPath.Interface;
            void Summarize()
            {
                messenger.SendData(algorithm, path, Tokens.History, Tokens.Statistics);
            }
            using (Disposable.Use(Summarize))
            {
                PrepareForPathfinding(algorithm);
                path = algorithm.FindPath();
                var vertices = path.Select(graph.Get);
                vertices.ForEach(v => v.VisualizeAsPath());
            }
            input.Input();
        }

        private void OnGraphCreated(DataMessage<Graph2D<Vertex>> msg)
        {
            graph = msg.Value;
        }

        private void PrepareForPathfinding(PathfindingProcess algorithm)
        {
            messenger.SendData(algorithm.ToString(), Tokens.Screen);
            messenger.SendData<PathfindingProcess>(algorithm, Tokens.Visualization, Tokens.History, Tokens.Statistics);
            messenger.SendData(algorithm, rangeBuilder.Range, Tokens.History);
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<IAlgorithmFactory<PathfindingProcess>>(this, Tokens.Pathfinding, FindPath);
            messenger.RegisterGraph(this, Tokens.Common, OnGraphCreated);
            messenger.Register<ClearColorsMessage>(this, _ => ClearColors());
        }
    }
}