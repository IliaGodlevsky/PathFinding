using Autofac;
using System;
using System.Windows.Forms;
using WindowsFormsVersion.Configure;

namespace WindowsFormsVersion
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var container = ContainerConfigure.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(scope.Resolve<Form>());
            }
        }
    }
}
