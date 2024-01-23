using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Settings;
using Microsoft.Data.Sqlite;

namespace Pathfinding.App.Console.DAL.UOF.Factories
{
    internal sealed class SqliteInFileUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static readonly string ConnectionString = GetConnectionString();

        public IUnitOfWork Create() => new SqliteUnitOfWork(ConnectionString);

        private static string GetConnectionString()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = Parametres.Default.SqliteConnectionString
            }; 
            return connectionStringBuilder.ToString();
        }
    }
}
