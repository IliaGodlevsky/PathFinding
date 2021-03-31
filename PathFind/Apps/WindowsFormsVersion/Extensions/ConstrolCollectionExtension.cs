using System;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace WindowsFormsVersion.Extensions
{
    internal static class ControlCollectionExtension
    {
        public static void RemoveBy(this ControlCollection collection,
            Func<Control, bool> predicate)
        {
            foreach (Control control in collection)
            {
                if (predicate(control))
                {
                    collection.Remove(control);
                }
            }
        }
    }
}
