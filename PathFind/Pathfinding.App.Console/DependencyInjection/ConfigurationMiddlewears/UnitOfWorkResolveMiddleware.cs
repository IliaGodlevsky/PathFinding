using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.DataAccess.UnitOfWorks.Factories;
using System;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class UnitOfWorkResolveMiddleware<T> : IResolveMiddleware
        where T : IUnitOfWorkFactory, new()
    {
        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var typed = new TypedParameter(typeof(IUnitOfWorkFactory), new T());
            context.ChangeParametres(typed);
            next(context);
        }
    }
}
