using AssembleClassesLib.Interface;
using System.Configuration;

namespace WindowsFormsVersion.Model
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
