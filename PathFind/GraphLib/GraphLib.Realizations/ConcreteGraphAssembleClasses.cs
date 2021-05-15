using AssembleClassesLib.Attributes;
using AssembleClassesLib.Interface;
using Common.Extensions;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Factories.GraphAssembles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations
{
    public sealed class ConcreteGraphAssembleClasses : IAssembleClasses
    {
        public ConcreteGraphAssembleClasses(params IGraphAssemble[] graphAssembles)
        {
            this.graphAssembles = graphAssembles;
            assembleClasses = new Dictionary<string, IGraphAssemble>();
        }

        public IReadOnlyCollection<string> ClassesNames { get; private set; }

        public object Get(string name, params object[] ctorParametres)
        {
            return assembleClasses.TryGetValue(name, out var graphAssemble)
                ? graphAssemble
                : new NullGraphAssemble();
        }

        public void LoadClasses()
        {
            foreach (var graphAssemble in graphAssembles)
            {
                string className = GetClassName(graphAssemble.GetType());
                assembleClasses.Add(className, graphAssemble);
            }
            ClassesNames = assembleClasses.Keys.ToArray();
        }

        private string GetClassName(Type assembleClass)
        {
            var attribute = assembleClass.GetAttributeOrNull<ClassNameAttribute>();
            return attribute?.Name ?? assembleClass.FullName;
        }

        private readonly Dictionary<string, IGraphAssemble> assembleClasses;
        private readonly IGraphAssemble[] graphAssembles;
    }
}
