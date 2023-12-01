using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Shared.Executable;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pathfinding.App.Console.Units
{
    internal sealed class MainUnit : Unit, ICanRecieveMessage
    {
        private readonly IUndo undo;
        private readonly ILog log;

        private GraphField Field { get; set; } = GraphField.Empty;

        private IGraph<Vertex> Graph { get; set; } = Graph<Vertex>.Empty;

        public MainUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IUndo undo,
            ILog log) : base(menuItems)
        {
            this.undo = undo;
            this.log = log;
        }

        private bool IsGraphCreated() => Graph != Graph<Vertex>.Empty;

        private void DisplayGraph()
        {
            try
            {
                using (Cursor.HideCursor())
                {
                    Terminal.Clear();
                    Terminal.ForegroundColor = ConsoleColor.White;
                    Terminal.WriteLine(Graph);
                    Graph.RestoreVerticesVisualState();
                    Field.Display();
                }
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

        private void SetGraph(GraphMessage msg)
        {
            undo.Undo();
            Graph = msg.Graph;
            Field = new(Graph);
            DisplayGraph();
        }

        private void OnGraphChanged(GraphChangedMessage msg)
        {
            using (Cursor.UseCurrentPosition())
            {
                var position = new Point(0, 0);
                Cursor.SetPosition(position);
                Terminal.Write(new string(' ',
                    Terminal.WindowWidth));
                Cursor.SetPosition(position);
                Terminal.WriteLine(Graph);
            }
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Main.Bind(IsGraphCreated);
            messenger.RegisterGraph(this, Tokens.Main, SetGraph);
            messenger.Register<MainUnit, GraphChangedMessage>(this, token, OnGraphChanged);
        }
    }
}