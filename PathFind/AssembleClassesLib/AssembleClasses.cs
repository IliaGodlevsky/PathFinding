using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using AssembleClassesLib.Attributes;
using AssembleClassesLib.EventArguments;
using AssembleClassesLib.EventHandlers;
using AssembleClassesLib.Interface;
using Common.Extensions;

namespace AssembleClassesLib
{
    /// <summary>
    /// A class, that loads types from assembles
    /// </summary>
    public class AssembleClasses : IAssembleClasses
    {
        public event AssembleClassesEventHandler OnClassesLoaded;

        public AssembleClasses(string loadPath, SearchOption searchOption)
        {
            types = new Dictionary<string, Type>();
            this.loadPath = loadPath;
            this.searchOption = searchOption;
            ClassesNames = new string[] { };
        }

        public virtual void Dispose()
        {
            OnClassesLoaded = null;
        }

        public string[] ClassesNames { get; protected set; }

        public virtual object Get(string key, params object[] parametres)
        {
            return types.TryGetValue(key, out var type)
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
        public virtual void LoadClasses()
        {
            LoadClassesFromAssemble();
            ClassesNames = types.Keys.OrderBy(Key).ToArray();
            var args = new AssembleClassesEventArgs(ClassesNames);
            OnClassesLoaded?.Invoke(this, args);
        }

        protected virtual void LoadClassesFromAssemble()
        {
            types = Directory.GetFiles(loadPath, SearchPattern, searchOption)
                .Select(Assembly.LoadFrom)
                .SelectMany(Types)
                .DistinctBy(FullName)
                .ToDictionary(Description);
        }

        protected string Key(string key)
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

        private string Description(Type type)
        {
            var attribute = type.GetAttribute<ClassNameAttribute>();
            return attribute?.Name ?? type.FullName;
        }

        protected Dictionary<string, Type> types;
        private const string SearchPattern = "*.dll";
        private readonly string loadPath;
        private readonly SearchOption searchOption;
    }
}
