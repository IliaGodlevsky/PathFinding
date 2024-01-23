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
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Windows.Markup;

namespace Pathfinding.App.Console.DAL.Repositories.SqliteRepositories
{
    internal abstract class SqliteRepository<T> where T : class, IEntity
    {
        private sealed class PropertyName(string text) : ISqliteBuildAttribute
        {
            public int Order { get; } = 2;

            public string Text { get; } = text;
        }

        private sealed class PropertyType(Type type) : ISqliteBuildAttribute
        {
            private static readonly IReadOnlyDictionary<Type, string> CSharpToSQLiteTypeMap;

            public int Order { get; } = 3;

            public string Text { get; } = CSharpToSQLiteTypeMap[type];

            static PropertyType()
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
            }
        }

        protected const string Id = nameof(IEntity.Id);

        private static readonly Type Type = typeof(T);
        private static readonly IReadOnlyCollection<PropertyInfo> Properties;
        private static readonly IReadOnlyCollection<string> Names;

        private readonly static string DeleteQuery;
        private readonly static string SelectAllQuery;
        private readonly static string SelectQuery;
        private readonly static string InsertQuery;
        private readonly static string UpdateQuery;
        private readonly static string CreateTableScript;
        private readonly static string CreateIndexScript;

        protected readonly static string TableName;

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
            Properties = Type.GetProperties().ToReadOnly();
            Names = GetPropertiesNames();
            TableName = Type.GetAttributeOrDefault<TableAttribute>().Name;
            InsertQuery = GetInsertQuery();
            UpdateQuery = GetUpdateQuery();
            CreateTableScript = GetCreateTableScript();
            CreateIndexScript = GetCreateIndexScript();
            SelectAllQuery = $"SELECT * FROM {TableName}";
            SelectQuery = $"SELECT * FROM {TableName} WHERE {Id} = @{Id}";
            DeleteQuery = $"DELETE FROM {TableName} WHERE {Id} = @{Id}";
        }

        public virtual T Insert(T entity)
        {
            entity.Id = connection.QuerySingle<int>(InsertQuery, entity, transaction);
            return entity;
        }

        public virtual bool Update(T entity)
        {
            connection.Query(UpdateQuery, entity, transaction);
            return true;
        }

        public virtual bool Delete(int id)
        {
            var parametres = new { Id = id };
            connection.Query(DeleteQuery, parametres, transaction);
            var present = connection.Query<T>(SelectQuery, parametres, transaction);
            return !present.Any();
        }

        public virtual T Read(int id)
        {
            return connection.QuerySingle<T>(SelectQuery, new { Id = id }, transaction);
        }

        public virtual IEnumerable<T> Insert(IEnumerable<T> entities)
        {
            var values = entities.ToReadOnly();
            if (values.Count > 0)
            {
                var insertQuery = GetBulkInsertQuery(values);
                connection.Execute(insertQuery, transaction: transaction);
                var instertedScript = $"SELECT * FROM {TableName} " +
                    $"ORDER BY {Id} DESC LIMIT {values.Count}";
                var inserted = connection
                    .Query<T>(instertedScript, transaction: transaction)
                    .Reverse()
                    .ToReadOnly();
                for (int i = 0; i < values.Count; i++)
                {
                    values[i].Id = inserted[i].Id;
                }
            }
            return values;
        }

        public IEnumerable<T> GetAll()
        {
            return connection.Query<T>(SelectAllQuery, transaction: transaction);
        }

        private static IReadOnlyCollection<string> GetPropertiesNames()
        {
            return Properties
                .Where(p => !Attribute.IsDefined(p, typeof(IdentityAttribute)))
                .Select(p => p.Name)
                .ToReadOnly();
        }

        private static string GetCreateTableScript()
        {
            string attributes = Properties.Select(p => 
                    p.GetCustomAttributes<SqliteBuildAttribute>()
                    .OfType<ISqliteBuildAttribute>()
                    .Append(new PropertyName(p.Name))
                    .Append(new PropertyType(p.PropertyType))
                    .OrderBy(x => x.Order)
                    .Select(x => x.Text)
                    .To(texts => string.Join(" ", texts)))
                .To(props => string.Join(",\n", props));
            return $"CREATE TABLE IF NOT EXISTS {TableName} \n({attributes})";
        }

        private static string GetCreateIndexScript()
        {
            return Properties
                .Where(p => Attribute.IsDefined(p, typeof(IndexAttribute)))
                .Select(p => $"CREATE INDEX IF NOT EXISTS idx_{TableName}_{p.Name} ON {TableName}({p.Name})")
                .To(lines => string.Join(";\n", lines));
        }

        private static string GetInsertQuery()
        {
            string props = string.Join(", ", Names);
            string values = string.Join(", ", Names.Select(p => $"@{p}"));
            string query = $"INSERT INTO {TableName} ({props}) " +
                $"VALUES ({values}); SELECT LAST_INSERT_ROWID()";
            return query;
        }

        private static string GetUpdateQuery()
        {
            var values = Names.Select(p => $"{p} = @{p}")
                .To(props => string.Join(", ", props));
            return $"UPDATE {TableName} SET {values} WHERE {Id} = @{Id}";
        }

        private static string GetBulkInsertQuery(IEnumerable<T> items)
        {
            var properties = Properties
                .Where(p => !Attribute.IsDefined(p, typeof(IdentityAttribute)))
                .ToReadOnly();
            string result = items.Select(i =>
            {
                var props = properties
                    .Select(p => p.GetValue(i))
                    .To(values => string.Join(", ", values));
                return string.Concat("(", props, ")");
            }).To(str => $"INSERT INTO {TableName} " +
                $"({string.Join(", ", Names)}) VALUES {string.Join(", ", str)}");
            return result;
        }
    }
}
