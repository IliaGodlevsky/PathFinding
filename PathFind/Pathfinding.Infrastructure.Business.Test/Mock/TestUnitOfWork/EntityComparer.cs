using Pathfinding.Domain.Core;
using Pathfinding.Shared.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace Pathfinding.Infrastructure.Business.Test.Mock.TestUnitOfWork
{
    internal sealed class EntityComparer<T>
        : Singleton<EntityComparer<T>, IEqualityComparer<IEntity<T>>>, IEqualityComparer<IEntity<T>>
    {
        private EntityComparer()
        {

        }

        public bool Equals(IEntity<T>? x, IEntity<T>? y)
        {
            if(x is not null && y is not null)
            {
                return x.Id?.Equals(y.Id) == true;
            }
            return false;
        }

        public int GetHashCode([DisallowNull] IEntity<T> obj)
        {
            return obj.Id?.GetHashCode() 
                ?? throw new ArgumentNullException(nameof(obj.Id));
        }
    }
}
