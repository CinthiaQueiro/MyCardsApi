using Dapper;
using Data.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Repositories
{
    public abstract class RepositoryBase<TEntity> :
        IRepositoryBase<TEntity> where TEntity : class
    {
        private Validation<TEntity> _validation;

        public virtual string _connectionString { get; set; }

        public RepositoryBase()
        {
            _validation = new Validation<TEntity>();
        }

        protected long Insert(string table, IEnumerable<string> columns, object dados)
        {
            var sql = "";
            sql += $"INSERT INTO {table} (";
            sql += string.Join(",", columns);
            sql += ") VALUES (@";
            sql += string.Join(",@", columns);
            sql += ");";
            sql += " SELECT SCOPE_IDENTITY();";

            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                var id = conexao.Query<long>(sql, dados).SingleOrDefault();
                return id;
            }
        }

    }
}
