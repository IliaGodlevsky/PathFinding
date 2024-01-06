using Dapper;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Repositories.SqliteRepositories
{
    internal abstract class SqliteRepository<T> where T : class, IEntity
    {
        private static IReadOnlyDictionary<Type, string> CSharpToSQLiteTypeMap { get; }

        protected readonly static string TableName;
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

            TableName = typeof(T).GetAttributeOrDefault<TableAttribute>().Name;
            InsertQuery = GetInsertQuery();
            UpdateQuery = GetUpdateQuery();
            CreateTableScript = GetCreateTableScript();
            CreateIndexScript = GetCreateIndexScript();
        }

        public T Insert(T entity)
        {
            entity.Id = connection.QuerySingle<int>(InsertQuery, entity, transaction);
            return entity;
        }

        public IEnumerable<T> Insert(IEnumerable<T> entities)
        {
            return entities.ForEach(x => Insert(x));
        }

        public bool Update(T entity)
        {
            connection.Query(UpdateQuery, entity, transaction);
            return true;
        }

        private static IReadOnlyCollection<string> GetPropertiesNames()
        {
            return typeof(T).GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)))
                .Select(p => p.Name)
                .ToReadOnly();
        }

        private static string GetCreateTableScript()
        {
            var properties = typeof(T).GetProperties()
                .Select(p =>
                {
                    var keyAttribute = p.GetAttributeOrDefault<KeyAttribute>();
                    var requiredAttribute = p.GetAttributeOrDefault<RequiredAttribute>();
                    var propertyName = p.Name + " " + CSharpToSQLiteTypeMap[p.PropertyType];
                    propertyName += requiredAttribute is not null ? " NOT NULL" : string.Empty;
                    propertyName += keyAttribute is not null ? " PRIMARY KEY AUTOINCREMENT" : string.Empty;
                    return propertyName;
                });
            string query = $"CREATE TABLE IF NOT EXISTS {TableName}" +
                $" ({string.Join(", ", properties)});";
            return query;
        }

        private static string GetCreateIndexScript()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(IndexFieldAttribute)))
                .Select(p => p.Name)
                .ToArray();
            var indexName = $"idx_{string.Concat(properties)}";
            var fields = string.Join(",", properties);
            string query = $"CREATE INDEX IF NOT EXISTS " +
                $"{indexName} ON {TableName}({fields});";
            return query;
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
            var props = properties.Select(p => $"{p} = @{p}");
            var values = string.Join(", ", props);
            string query = $"UPDATE {TableName} SET " +
                $"{values} WHERE {nameof(IEntity.Id)} = @{nameof(IEntity.Id)}";
            return query;
        }
    }
}
