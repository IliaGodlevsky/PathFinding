using Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Common
{
    public class ClassLoader<TBase> 
        where TBase : class
    {
        public static ClassLoader<TBase> Instance
        {
            get
            {
                if (instance == null)
                    instance = new ClassLoader<TBase>();
                return instance;
            }
        }

        public IEnumerable<Type> LoadTypesFromAssembles(
            string path, 
            string searchPattern = "*.dll",
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (Directory.Exists(path))
            {
                return Directory
                      .GetFiles(path, searchPattern, searchOption)
                      .Select(Assembly.LoadFrom)
                      .SelectMany(Types)
                      .DistinctBy(Name);
            }

            throw new DirectoryNotFoundException($"Directory {path} was not found");
        }

        private bool IsValidType(Type type)
        {
            return typeof(TBase).IsAssignableFrom(type);
        }

        private IEnumerable<Type> Types(Assembly assembly)
        {
            return assembly.GetTypes().Where(IsValidType);
        }

        private string Name(Type type)
        {
            return type.FullName;
        }

        private ClassLoader()
        {

        }

        private static ClassLoader<TBase> instance = null;
    }
}
