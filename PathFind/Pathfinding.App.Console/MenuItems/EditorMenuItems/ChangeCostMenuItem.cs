using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [LowPriority]
    internal sealed class ChangeCostMenuItem : NavigateThroughVerticesMenuItem
    {
        public ChangeCostMenuItem(IInput<ConsoleKey> keyInput,
            IService service)
            : base(keyInput, service)
        {
        }

        public override void Execute()
        {
            base.Execute();
            service.UpdateVertices(processed, graph.Id);
            processed.Clear();
        }

        public override string ToString()
        {
            return Languages.ChangeCost;
        }

        protected override VertexActions GetActions()
        {
            return new (string, IVertexAction)[]
            {
                (nameof(Keys.Default.IncreaseCost), new IncreaseCostAction()),
                (nameof(Keys.Default.DecreaseCost), new DecreaseCostAction())
            };
        }
    }
}
