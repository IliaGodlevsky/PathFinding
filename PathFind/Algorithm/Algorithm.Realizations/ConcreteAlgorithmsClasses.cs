using Algorithm.Common;
using Algorithm.Interfaces;
using AssembleClassesLib.Realizations;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Algorithm.Realizations
{
    public sealed class ConcreteAssembleAlgorithmClasses : AssembleClasses
    {
        /// <summary>
        /// Returns algorithm according to 
        /// <paramref name="key"></paramref>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parametres"></param>
        /// <returns>An instance of algorithm if 
        /// <paramref name="key"></paramref> exists and
        /// <see cref="DefaultAlgorithm"></see> when doesn't</returns>
        public override object Get(string key, params object[] parametres)
        {
            return base.Get(key, parametres) ?? new DefaultAlgorithm();
        }

        public ConcreteAssembleAlgorithmClasses(string path,
            SearchOption searchOption = SearchOption.AllDirectories)
            : base(path, searchOption)
        {
            baseType = typeof(IAlgorithm);
        }

        protected override void LoadClassesFromAssemble()
        {
            base.LoadClassesFromAssemble();
            types = types
                .Where(IsConcreteAlgorithm)
                .ToDictionary(ClassName, Type);
        }

        private string ClassName(KeyValuePair<string, Type> algo)
        {
            return algo.Key;
        }

        private Type Type(KeyValuePair<string, Type> algo)
        {
            return algo.Value;
        }

        private bool IsConcreteAlgorithm(KeyValuePair<string, Type> algo)
        {
            return !algo.Value.IsFilterable()
                   && !algo.Value.IsAbstract
                   && baseType.IsAssignableFrom(algo.Value);
        }

        private readonly Type baseType;
    }
}