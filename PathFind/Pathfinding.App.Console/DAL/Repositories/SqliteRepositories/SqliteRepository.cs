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

        private static bool IsTableCreated { get; set; }

        protected static string TableName { get; }

        protected static string InsertQuery { get; }

        protected static string UpdateQuery { get; }

        protected readonly IDbConnection connection;
        protected readonly IDbTransaction transaction;

        protected SqliteRepository(IDbConnection connection,
            IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
            if (!IsTableCreated)
            {
                string createTable = GetCreateTableScript();
                connection.Execute(createTable, transaction: this.transaction);
                string createIndex = GetCreateIndexScript();
                connection.Execute(createIndex, transaction: this.transaction);
                IsTableCreated = true;
            }
        }

        static SqliteRepository()
        {
            TableName = typeof(T).GetAttributeOrDefault<TableAttribute>().Name;
            InsertQuery = GetInsertQuery();
            UpdateQuery = GetUpdateQuery();
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
        }

        public T Insert(T entity)
        {
            entity.Id = connection.QuerySingle<int>(InsertQuery, entity, transaction);
            return entity;
        }

        public IEnumerable<T> Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
            return entities;
        }

        public bool Update(T entity)
        {
            connection.Query<T>(UpdateQuery, entity, transaction);
            return true;
        }

        private static IEnumerable<string> GetPropertiesNames()
        {
            return typeof(T).GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)))
                .Select(p => p.Name);
        }

        protected string GetCreateTableScript()
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

        protected string GetCreateIndexScript()
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
            var properties = GetPropertiesNames().ToArray();
            string props = string.Join(", ", properties);
            var values = string.Join(", ", properties.Select(p => $"@{p}"));
            string query = $"INSERT INTO {TableName} ({props}) " +
                $"VALUES ({values}); SELECT last_insert_rowid()";
            return query;
        }

        private static string GetUpdateQuery()
        {
            var properties = GetPropertiesNames().ToArray();
            var props = properties.Select(p => $"{p} = @{p}").ToArray();
            var values = string.Join(", ", props);
            string query = $"UPDATE {TableName} SET " +
                $"{values} WHERE {nameof(IEntity.Id)} = Id";
            return query;
        }
    }
}
