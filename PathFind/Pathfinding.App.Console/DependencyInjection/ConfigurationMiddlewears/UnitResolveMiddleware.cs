using Autofac.Core.Resolving.Pipeline;
using System;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class UnitResolveMiddleware : IResolveMiddleware
    {
        private readonly string metadataKey;
        private readonly Type type;
        private readonly IParametresFactory factory;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public UnitResolveMiddleware(string metadataKey,
            Type type, IParametresFactory factory)
        {
            this.metadataKey = metadataKey;
            this.type = type;
            this.factory = factory;
        }

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var parametres = factory.GetParameters(context, type);
            context.ChangeParameters(parametres);
            next(context);
        }
    }
}
