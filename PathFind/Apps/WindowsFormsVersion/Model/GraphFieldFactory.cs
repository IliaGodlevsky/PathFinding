using GraphLib.Base;
using GraphLib.Interfaces;
using System.Drawing;
using WindowsFormsVersion.View;

namespace WindowsFormsVersion.Model
{
    internal sealed class GraphFieldFactory : BaseGraphFieldFactory, IGraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new WinFormsGraphField { Location = new Point(4, 90) };
        }
    }
}
