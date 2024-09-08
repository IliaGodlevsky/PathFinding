using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.Messages.View
{
    internal interface IOpenViewMessage
    {
        void Open(Terminal.Gui.View view);
    }
}
