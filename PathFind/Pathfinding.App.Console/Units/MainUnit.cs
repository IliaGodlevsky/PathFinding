using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.Logging.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pathfinding.App.Console.Units
{
    using FieldFactory = IGraphFieldFactory<Vertex, GraphField>;

    internal sealed class MainUnit : Unit, ICanRecieveMessage
    {
        private readonly FieldFactory fieldFactory;
        private readonly IUndo undo;
        private readonly ILog log;

        private GraphField GraphField { get; set; } = GraphField.Empty;

        private IGraph<Vertex> Graph { get; set; } = Graph<Vertex>.Empty;

        public MainUnit(IReadOnlyCollection<IMenuItem> menuItems,
            FieldFactory fieldFactory,
            IUndo undo,
            ILog log) : base(menuItems)
        {
            this.undo = undo;
            this.log = log;
            this.fieldFactory = fieldFactory;
        }

        private bool IsGraphCreated() => Graph != Graph<Vertex>.Empty;

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

        private void SetGraph(IGraph<Vertex> graph)
        {
            undo.Undo();
            Graph = graph;
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
            var token = Tokens.Main.Bind(IsGraphCreated);
            messenger.RegisterGraph(this, Tokens.Main, SetGraph);
            messenger.Register<GraphChangedMessage>(this, token, OnGraphChanged);
        }
    }
}