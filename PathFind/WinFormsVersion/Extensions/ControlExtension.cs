using System.Windows.Forms;
using WinFormsVersion.Model;

namespace WinFormsVersion.Extensions
{
    public static class ControlExtension
    {
        public static bool IsGraphField(this Control control)
        {
            return control.GetType() == typeof(WinFormsGraphField);
        }
    }
}
