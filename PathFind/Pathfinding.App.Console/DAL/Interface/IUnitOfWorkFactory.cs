using Pathfinding.App.Console.DAL.UOF;
using Pathfinding.App.Console.Settings;
using System.IO;
using System;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }

    internal sealed class SqliteUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new SqliteUnitOfWork();
        }
    }

    internal sealed class LiteDbUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new LiteDbUnitOfWork(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            string connectionString = Parametres.Default.LiteDbConnectionString;
            return Path.Combine(Environment.CurrentDirectory, connectionString);
        }
    }

    internal sealed class InMemoryUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static readonly MemoryStream Memory = new();

        public IUnitOfWork Create()
        {
            return new LiteDbUnitOfWork(Memory);
        }
    }
}
