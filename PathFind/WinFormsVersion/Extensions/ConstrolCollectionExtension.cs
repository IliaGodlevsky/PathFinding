using System;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace WinFormsVersion.Extensions
{
    public static class ConstrolCollectionExtension
    {
        public static void RemoveBy(this ControlCollection collection, 
            Func<Control,bool> predicate)
        {
            foreach(Control control in collection)
            {
                if (predicate(control))
                    collection.Remove(control);
            }
        }
    }
}
