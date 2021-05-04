using AssembleClassesLib.Interface;
using System.Configuration;

namespace ConsoleVersion.Model
{
    internal sealed class AssembleLoadPath : IAssembleLoadPath
    {
        public string LoadPath { get; }

        public AssembleLoadPath()
        {
            LoadPath = ConfigurationManager.AppSettings["pluginsPath"];
        }
    }
}
