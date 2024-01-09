using Dapper;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console.DAL.Repositories.SqliteRepositories
{
    internal abstract class SqliteRepository<T> where T : class, IEntity
    {
        private const string Id = nameof(IEntity.Id);

        private static readonly Type Type = typeof(T);

        private static IReadOnlyDictionary<Type, string> CSharpToSQLiteTypeMap { get; }

        protected readonly static string TableName;
        protected readonly static string DeleteQuery;
        protected readonly static string SelectAllQuery;
        protected readonly static string SelectQuery;
        protected readonly static string InsertQuery;
        protected readonly static string UpdateQuery;
        protected readonly static string CreateTableScript;
        protected readonly static string CreateIndexScript;

        protected readonly IDbConnection connection;
        protected readonly IDbTransaction transaction;

        protected SqliteRepository(IDbConnection connection,
            IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
            connection.Execute(CreateTableScript, transaction: this.transaction);
            connection.Execute(CreateIndexScript, transaction: this.transaction);
        }

        static SqliteRepository()
        {
            CSharpToSQLiteTypeMap = new Dictionary<Type, string>
            {
                { typeof(int), "INTEGER" },
                { typeof(long), "INTEGER" },
                { typeof(short), "INTEGER" },
                { typeof(byte), "INTEGER" },
                { typeof(uint), "INTEGER" },
                { typeof(ulong), "INTEGER" },
                { typeof(ushort), "INTEGER" },
                { typeof(sbyte), "INTEGER" },
                { typeof(float), "REAL" },
                { typeof(double), "REAL" },
                { typeof(decimal), "NUMERIC" },
                { typeof(bool), "INTEGER" },
                { typeof(string), "TEXT" },
                { typeof(char), "TEXT" },
                { typeof(DateTime), "TEXT" },
                { typeof(byte[]), "BLOB" }
            }.AsReadOnly();

            TableName = Type.GetAttributeOrDefault<TableAttribute>().Name;
            InsertQuery = GetInsertQuery();
            UpdateQuery = GetUpdateQuery();
            CreateTableScript = GetCreateTableScript();
            CreateIndexScript = GetCreateIndexScript();
            SelectAllQuery = $"SELECT * FROM {TableName}";
            SelectQuery = $"SELECT * FROM {TableName} WHERE {Id} = @{Id}";
            DeleteQuery = $"DELETE FROM {TableName} WHERE {Id} = @{Id}";
        }

        public T Insert(T entity)
        {
            entity.Id = connection.QuerySingle<int>(InsertQuery, entity, transaction);
            return entity;
        }

        public bool Update(T entity)
        {
            connection.Query(UpdateQuery, entity, transaction);
            return true;
        }

        public bool Delete(int id)
        {
            connection.Query(DeleteQuery, new { Id = id }, transaction);
            return true;
        }

        public T Read(int id)
        {
            return connection.QuerySingle<T>(SelectQuery, new { Id = id }, transaction);
        }

        public IEnumerable<T> Insert(IEnumerable<T> entities)
        {
            return entities.ForEach(x => Insert(x));
        }

        public IEnumerable<T> GetAll()
        {
            return connection.Query<T>(SelectAllQuery, transaction: transaction);
        }

        private static IReadOnlyCollection<string> GetPropertiesNames()
        {
            return Type.GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(IdentityAttribute)))
                .Select(p => p.Name)
                .ToReadOnly();
        }

        private static string GetCreateTableScript()
        {
            return Type.GetProperties().Select(p =>
            {
                return p.GetCustomAttributes<SqliteBuildAttribute>()
                    .OrderBy(p => p.Order)
                    .Select(x => x.Line)
                    .Prepend(CSharpToSQLiteTypeMap[p.PropertyType])
                    .Prepend(p.Name)
                    .To(lines => string.Join(" ", lines));
            }).To(props => $"CREATE TABLE IF NOT EXISTS {TableName} \n({string.Join(",\n", props)});");
        }

        private static string GetCreateIndexScript()
        {
            return Type.GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(IndexFieldAttribute)))
                .Select(x => $"CREATE INDEX IF NOT EXISTS idx_{TableName}_{x.Name} ON {TableName}({x.Name})")
                .To(lines => string.Join(";\n", lines));
        }

        private static string GetInsertQuery()
        {
            var properties = GetPropertiesNames();
            string props = string.Join(", ", properties);
            var values = string.Join(", ", properties.Select(p => $"@{p}"));
            string query = $"INSERT INTO {TableName} ({props}) " +
                $"VALUES ({values}); SELECT LAST_INSERT_ROWID()";
            return query;
        }

        private static string GetUpdateQuery()
        {
            var properties = GetPropertiesNames();
            var props = properties.Select(p => $"{p} = @{p}").ToReadOnly();
            var values = string.Join(", ", props);
            return $"UPDATE {TableName} SET {values} WHERE {Id} = @{Id}";
        }
    }
}
