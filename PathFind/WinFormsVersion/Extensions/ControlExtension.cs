using GraphLibrary.Model;
using System.Windows.Forms;

namespace WinFormsVersion.Extensions
{
    public static class ControlExtension
    {
        public static bool IsGraphField(this Control control)
        {
            return (control as IGraphField) != null;
        }
    }
}
