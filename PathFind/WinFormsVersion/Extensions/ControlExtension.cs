using GraphLibrary.GraphField;
using System.Windows.Forms;

namespace WinFormsVersion.Extensions
{
    internal static class ControlExtension
    {
        public static bool IsGraphField(this Control control)
        {
            return (control as IGraphField) != null;
        }
    }
}
