﻿using GraphLib.GraphField;
using System.Drawing;
using WindowsFormsVersion.View;

namespace WindowsFormsVersion.Model
{
    internal class GraphFieldFactory : GraphLib.GraphFieldCreating.BaseGraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new WinFormsGraphField() { Location = new Point(4, 90) };
        }
    }
}
