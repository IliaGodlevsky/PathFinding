using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Shared.Executable;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Units
{
    internal sealed class MainUnit(IReadOnlyCollection<IMenuItem> menuItems,
        IInput<Answer> input,
        IUndo undo,
        ILog log) : Unit(menuItems), ICanRecieveMessage
    {
        private readonly IUndo undo = undo;
        private readonly ILog log = log;
        private readonly Lazy<IMenuItem> exit 
            = new(() => new AnswerExitMenuItem(input));

        protected override IMenuItem ExitMenuItem => exit.Value;

        private GraphField Field { get; set; } = GraphField.Empty;

        private IGraph<Vertex> Graph { get; set; } = Graph<Vertex>.Empty;

        private bool IsGraphCreated() => Graph != Graph<Vertex>.Empty;

        private void DisplayGraph()
        {
            try
            {
                using (Cursor.HideCursor())
                {
                    Terminal.Clear();
                    Terminal.ForegroundColor = ConsoleColor.White;
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
            Graph = msg.Graph.Graph;
            Field = new(Graph);
            DisplayGraph();
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Main.Bind(IsGraphCreated);
            messenger.RegisterGraph(this, Tokens.Main, SetGraph);
        }
    }
}