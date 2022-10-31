using GraphLib.Interfaces;
using System.Windows.Forms;
using WindowsFormsVersion.Model;

namespace WindowsFormsVersion.Extensions
{
    internal static class ControlExtension
    {
        public static bool IsGraphField(this Control control)
        {
            return control is IGraphField<Vertex>;
        }
    }
}
