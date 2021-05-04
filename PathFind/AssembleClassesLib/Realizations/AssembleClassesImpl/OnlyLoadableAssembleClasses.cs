using AssembleClassesLib.Extensions;
using AssembleClassesLib.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssembleClassesLib.Realizations.AssembleClassesImpl
{
    public class OnlyLoadableAssembleClasses : AssembleClasses
    {
        public OnlyLoadableAssembleClasses(string loadPath, SearchOption searchOption, ILoadMethod loadMethod)
            : base(loadPath, searchOption, loadMethod)
        {

        }

        public OnlyLoadableAssembleClasses(string loadPath, IAssembleSearchOption searchOption, ILoadMethod loadMethod)
            : this(loadPath, searchOption.SearchOption, loadMethod)
        {

        }

        public OnlyLoadableAssembleClasses(IAssembleLoadPath loadPath, IAssembleSearchOption searchOption, ILoadMethod loadMethod)
            : this(loadPath.LoadPath, searchOption.SearchOption, loadMethod)
        {

        }

        public OnlyLoadableAssembleClasses(IAssembleLoadPath loadPath, SearchOption searchOption, ILoadMethod loadMethod)
            : this(loadPath.LoadPath, searchOption, loadMethod)
        {

        }

        protected override void LoadClassesFromAssemble()
        {
            base.LoadClassesFromAssemble();
            types = types
                .Where(IsLoadable)
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

        private bool IsLoadable(KeyValuePair<string,Type> type)
        {
            return type.Value.IsLoadable();
        }

    }
}
