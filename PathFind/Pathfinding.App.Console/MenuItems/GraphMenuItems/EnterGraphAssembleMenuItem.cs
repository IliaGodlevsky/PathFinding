using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Primitives.Attributes;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    using GraphAssemble = IGraphAssemble<Graph2D<Vertex>, Vertex>;

    [Order(2)]
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
            string message = Languages.GraphAssembleChoiceMsg;
            using (Cursor.UseCurrentPositionWithClean())
            {
                menuList.Display();
                int index = input.Input(message, range) - 1;
                messenger.Send(new ChooseGraphAssembleMessage(assembles[index]));
            }
        }

        public override string ToString()
        {
            return Languages.ChooseGraphAssemble;
        }
    }
}
