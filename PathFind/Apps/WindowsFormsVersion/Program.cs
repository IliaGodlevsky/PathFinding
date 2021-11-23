using Autofac;
using System;
using System.Windows.Forms;
using WindowsFormsVersion.Configure;
using WindowsFormsVersion.Forms;

namespace WindowsFormsVersion
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var container = ContainerConfigure.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainWindow>());
        }
    }
}
