using Pathfinding.App.Console.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DataAccess
{
    internal interface IStorage<TEntity, TId> 
        where TEntity : IEntity<TId>
    {
        TEntity Create(TEntity entity);

        TEntity Read(TId id);

        void Update(TEntity entity);

        TId Delete(TEntity entity);
    }
}
