using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using System;

namespace Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears
{
    internal sealed class UnitOfWorkResolveMiddleware<T> : IResolveMiddleware
        where T : IUnitOfWork, new()
    {
        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var typed = new TypedParameter(typeof(IUnitOfWork), new T());
            context.ChangeParametres(typed);
            next(context);
        }
    }
}
