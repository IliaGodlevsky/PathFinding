using Autofac;
using ConsoleVersion.Configure;
using ConsoleVersion.InputClass;
using ConsoleVersion.InputClass.Interface;
using ConsoleVersion.View.Interface;

namespace ConsoleVersion
{
    internal class Program
    {
        static Program()
        {
            Input = ValueInput.Instance;
        }

        public static IValueInput Input { get; }

        private static void Main(string[] args)
        {
            var container = ContainerConfigure.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var mainView = scope.Resolve<IView>();
                mainView.Start();
            }
        }
    }
}