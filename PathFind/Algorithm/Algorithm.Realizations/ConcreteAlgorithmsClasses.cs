using Algorithm.Base;
using Algorithm.Common;
using AssembleClassesLib;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Algorithm.Interfaces;

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
            return base.Get(key, parametres) ?? BaseAlgorithm.Default;
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
                .Where(IsAlgorithm)
                .ToDictionary(Description, Type);
        }

        private string Description(KeyValuePair<string, Type> algo)
        {
            return algo.Key;
        }

        private Type Type(KeyValuePair<string, Type> algo)
        {
            return algo.Value;
        }

        private bool IsAlgorithm(KeyValuePair<string,Type> algo)
        {
            return !algo.Value.IsFilterable()
                   && !algo.Value.IsAbstract
                   && IsDerived(algo.Value);
        }

        private bool IsDerived(Type type)
        {
            return baseType.IsAssignableFrom(type);
        }

        private readonly Type baseType;
    }
}