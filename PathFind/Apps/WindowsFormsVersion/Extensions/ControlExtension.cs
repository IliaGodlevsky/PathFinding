﻿using GraphLib.Interfaces;
using System.Windows.Forms;

namespace WindowsFormsVersion.Extensions
{
    internal static class ControlExtension
    {
        public static bool IsGraphField(this Control control)
        {
            return control is IGraphField;
        }
    }
}
