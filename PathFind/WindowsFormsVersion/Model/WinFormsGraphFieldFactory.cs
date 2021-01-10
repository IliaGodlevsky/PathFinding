﻿using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;
using System.Drawing;

namespace WindowsFormsVersion.Model
{
    internal class WinFormsGraphFieldFactory : GraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new WinFormsGraphField() { Location = new Point(4, 90) };
        }
    }
}