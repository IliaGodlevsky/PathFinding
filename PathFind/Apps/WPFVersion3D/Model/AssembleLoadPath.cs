using AssembleClassesLib.Interface;
using System.Configuration;

namespace WPFVersion3D.Model
{
    internal sealed class AssembleLoadPath : IAssembleLoadPath
    {
        public AssembleLoadPath()
        {
            LoadPath = ConfigurationManager.AppSettings["pluginsPath"];
        }

        public string LoadPath { get; }
    }
}
