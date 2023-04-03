using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pathfinding.App.Console.Units
{
    using FieldFactory = IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>;

    internal sealed class MainUnit : Unit, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly FieldFactory fieldFactory;
        private readonly IUndo undo;
        private readonly ILog log;

        private GraphField GraphField { get; set; } = GraphField.Empty;

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public MainUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            FieldFactory fieldFactory,
            IMessenger messenger,
            IUndo undo,
            ILog log)
            : base(menuItems, conditioned)
        {
            this.undo = undo;
            this.log = log;
            this.messenger = messenger;
            this.fieldFactory = fieldFactory;
        }

        private bool IsGraphCreated() => Graph != Graph2D<Vertex>.Empty;

        private void DisplayGraph()
        {
            try
            {
                System.Console.Clear();
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(Graph);
                GraphField.Display();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                log.Warn(ex);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void SetGraph(DataMessage<Graph2D<Vertex>> msg)
        {
            undo.Undo();
            Graph = msg.Value;
            GraphField = fieldFactory.CreateGraphField(Graph);
            DisplayGraph();
        }

        private void OnGraphChanged(GraphChangedMessage msg)
        {
            using (Cursor.UseCurrentPosition())
            {
                var position = new Point(0, 0);
                Cursor.SetPosition(position);
                System.Console.Write(new string(' ',
                    System.Console.BufferWidth));
                Cursor.SetPosition(position);
                System.Console.WriteLine(Graph);
            }
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = ConditionToken.Create(IsGraphCreated, Tokens.Main);
            messenger.RegisterGraph(this, Tokens.Main, SetGraph);
            messenger.Register<GraphChangedMessage>(this, token, OnGraphChanged);
        }
    }
}