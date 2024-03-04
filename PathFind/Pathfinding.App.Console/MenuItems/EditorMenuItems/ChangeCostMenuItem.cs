using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Settings;
using Shared.Extensions;
using System;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [LowPriority]
    internal sealed class ChangeCostMenuItem(IInput<ConsoleKey> keyInput,
        IService service) : NavigateThroughVerticesMenuItem(keyInput, service)
    {
        public async override void Execute()
        {
            base.Execute();
            var vertices = processed.ToReadOnly();
            processed.Clear();
            await Task.Run(() => service.UpdateVertices(vertices, graph.Id));
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
