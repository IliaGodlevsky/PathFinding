using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    using GraphAssemble = IGraphAssemble<Graph2D<Vertex>, Vertex>;

    [HighPriority]
    internal sealed class EnterGraphAssembleMenuItem : GraphMenuItem
    {
        private readonly IReadOnlyList<GraphAssemble> assembles;

        public EnterGraphAssembleMenuItem(IReadOnlyList<GraphAssemble> assembles,
            IMessenger messenger, IInput<int> input)
            : base(messenger, input)
        {
            this.assembles = assembles;
        }

        public override void Execute()
        {
            var menuList = assembles.CreateMenuList(columnsNumber: 1);
            var range = new InclusiveValueRange<int>(assembles.Count, 1);
            string menu = Languages.GraphAssembleChoiceMsg + "\n" + menuList;
            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = input.Input(menu, range) - 1;
                messenger.SendData(assembles[index], Tokens.Graph);
            }
        }

        public override string ToString()
        {
            return Languages.ChooseGraphAssemble;
        }
    }
}
