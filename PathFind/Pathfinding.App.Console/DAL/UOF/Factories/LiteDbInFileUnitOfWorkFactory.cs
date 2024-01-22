using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Settings;
using System;
using System.IO;

namespace Pathfinding.App.Console.DAL.UOF.Factories
{
    internal sealed class LiteDbInFileUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static readonly string ConnectionString = GetConnectionString();

        public IUnitOfWork Create() => new LiteDbUnitOfWork(ConnectionString);

        private static string GetConnectionString()
        {
            string connectionString = Parametres.Default.SqliteConnectionString;
            return Path.Combine(Environment.CurrentDirectory, connectionString);
        }
    }
}
