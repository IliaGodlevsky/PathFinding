using AssembleClassesLib.Attributes;
using AssembleClassesLib.Interface;
using System;
using System.IO;
using System.Reflection;

namespace AssembleClassesLib.Tests.Infrastructure
{
    [NotLoadable]
    internal sealed class AssempleLoadPath : IAssembleLoadPath
    {
        public AssempleLoadPath()
        {
            loadPath = new Lazy<string>(() =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                string codeBase = assembly.CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            });
        }

        public string LoadPath => loadPath.Value;

        private readonly Lazy<string> loadPath;
    }
}
