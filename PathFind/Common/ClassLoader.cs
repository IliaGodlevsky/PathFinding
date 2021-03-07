using Common.Enums;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// A class, that loades types from assembles, 
    /// that are assignable to <typeparamref name="TBase"/> 
    /// and match <see cref="LoadOption"/>
    /// </summary>
    public class ClassLoader<TBase> where TBase : class
    {
        public static ClassLoader<TBase> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClassLoader<TBase>();
                }

                return instance;
            }
        }

        /// <summary>
        /// Fetches types from assembles in folder
        /// that match <paramref name="loadOption"/>
        /// and that are assignable from or 
        /// equal to <typeparamref name="TBase"/>
        /// </summary>
        /// <param name="assemblesPath"></param>
        /// <param name="loadOption"></param>
        /// <param name="searchOption"></param>
        /// <returns> An array of types that
        /// match <paramref name="loadOption"/></returns>
        /// <exception cref="DirectoryNotFoundException">thrown
        /// when directory doesn't exist</exception>
        ///  <exception cref="ArgumentException"> thrown when 
        ///  path is a string with zero length, contains only 
        ///  white spaces of contains unallowed symbols</exception>
        ///  <exception cref="ArgumentNullException"/>
        ///  <exception cref="ArgumentOutOfRangeException">thrown
        ///  where <paramref name="searchPattern"/>doensn't belong to
        ///  <see cref="SearchOption"/></exception>
        ///  <exception cref="UnauthorizedAccessException"/>
        ///  <exception cref="PathTooLongException"/>
        ///  <exception cref="IOException"/>
        public IEnumerable<Type> FetchTypes(
            string assemblesPath,
            LoadOption loadOption = LoadOption.Hierarchy,
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            string searchPattern = "*.dll";
            this.loadOption = loadOption;
            return Directory
                  .GetFiles(
                    assemblesPath, 
                    searchPattern, 
                    searchOption)
                  .Select(Assembly.LoadFrom)
                  .SelectMany(Types)
                  .DistinctBy(FullName);
        }

        private bool IsValidType(Type type)
        {
            return filterFunctions[loadOption](type);
        }

        private IEnumerable<Type> Types(Assembly assembly)
        {
            return assembly.GetTypes().Where(IsValidType);
        }

        private string FullName(Type type)
        {
            return type.FullName;
        }

        private ClassLoader()
        {
            filterFunctions = new Dictionary<LoadOption, Func<Type, bool>>
            {
                { LoadOption.OnlyClass, type => typeof(TBase).FullName == type.FullName },
                { LoadOption.Hierarchy, type => typeof(TBase).IsAssignableFrom(type) }
            };
        }

        private readonly Dictionary<LoadOption, Func<Type, bool>> filterFunctions;
        private LoadOption loadOption;

        private static ClassLoader<TBase> instance = null;
    }
}
