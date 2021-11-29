using Autofac;
using System;
using System.Windows.Forms;
using WindowsFormsVersion.DependencyInjection;
using WindowsFormsVersion.Forms;

namespace WindowsFormsVersion
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(DI.Container.Resolve<MainWindow>());
        }
    }
}
