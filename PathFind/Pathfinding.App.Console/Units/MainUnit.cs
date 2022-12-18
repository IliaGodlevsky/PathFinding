using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Units
{
    using FieldFactory = IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>;

    internal sealed class MainUnit : Unit
    {
        private readonly IMessenger messenger;
        private readonly IInput<Answer> input;
        private readonly FieldFactory fieldFactory;
        private readonly IUndo undo;
        private readonly ILog log;

        private GraphField GraphField { get; set; } = GraphField.Empty;

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public MainUnit(IReadOnlyCollection<IMenuItem> menuItems, FieldFactory fieldFactory, IMessenger messenger,
            IInput<Answer> input, IUndo undo, ILog log)
            : base(menuItems)
        {
            this.undo = undo;
            this.log = log;
            this.messenger = messenger;
            this.input = input;
            this.fieldFactory = fieldFactory;
            this.messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainViewModel, SetGraph);
            this.messenger.Register<GraphChangedMessage>(this, OnGraphChanged);
        }

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

        private void SetGraph(GraphCreatedMessage message)
        {
            undo.Undo();
            Graph = message.Graph;
            GraphField = fieldFactory.CreateGraphField(Graph);
            DisplayGraph();
        }

        private void OnGraphChanged(GraphChangedMessage msg)
        {
            if (Graph != Graph2D<Vertex>.Empty)
            {
                using (Cursor.UseCurrentPosition())
                {
                    var position = new Coordinate2D(0, 0);
                    Cursor.SetPosition(position);
                    System.Console.Write(new string(' ',
                        System.Console.BufferWidth));
                    Cursor.SetPosition(position);
                    System.Console.WriteLine(Graph);
                }
            }
        }

        protected override IMenuItem GetExitMenuItem()
        {
            return new AnswerExitMenuItem(input);
        }
    }
}