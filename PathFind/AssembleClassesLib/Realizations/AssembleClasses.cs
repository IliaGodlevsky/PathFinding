using AssembleClassesLib.Attributes;
using AssembleClassesLib.Interface;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AssembleClassesLib.Realizations
{
    /// <summary>
    /// A class, that loads types from assemble(s)
    /// </summary>
    public class AssembleClasses : IAssembleClasses
    {
        public AssembleClasses(string loadPath, SearchOption searchOption)
        {
            types = new Dictionary<string, Type>();
            this.loadPath = loadPath;
            this.searchOption = searchOption;
            ClassesNames = new string[] { };
        }

        public IReadOnlyCollection<string> ClassesNames { get; protected set; }

        public virtual object Get(string name, params object[] parametres)
        {
            return types.TryGetValue(name, out var type)
                ? Activator.CreateInstance(type, parametres)
                : default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="TargetInvocationException"/>
        /// <exception cref="MethodAccessException"/>
        /// <exception cref="MemberAccessException"/>
        /// <exception cref="TypeLoadException"/>
        /// <exception cref="MissingMethodException"/>
        /// <exception cref="InvalidComObjectException"/>
        /// <exception cref="COMException"/>
        public void LoadClasses()
        {
            LoadClassesFromAssemble();
            ClassesNames = types.Keys.OrderBy(Key).ToArray();
        }

        protected virtual void LoadClassesFromAssemble()
        {
            types = Directory.GetFiles(loadPath, SearchPattern, searchOption)
                .Select(Assembly.LoadFrom)
                .SelectMany(Types)
                .DistinctBy(FullName)
                .ToDictionary(ClassName);
        }

        private string Key(string key)
        {
            return key;
        }

        private IEnumerable<Type> Types(Assembly assembly)
        {
            return assembly.GetTypes();
        }

        private string FullName(Type type)
        {
            return type.FullName;
        }

        private string ClassName(Type type)
        {
            var attribute = type.GetAttribute<ClassNameAttribute>();
            return attribute?.Name ?? type.FullName;
        }

        protected Dictionary<string, Type> types;
        private readonly string loadPath;
        private readonly SearchOption searchOption;

        private const string SearchPattern = "*.dll";
    }
}
