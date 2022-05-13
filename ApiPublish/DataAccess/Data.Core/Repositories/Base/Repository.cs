using Core.Common.Domain;
using Core.Common.RepoWrapper;
using Dapper;
using Data.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Core.Repositories.Base
{
    /// <summary>
    /// Base repository that wraps the Dapper Micro ORM
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository<T> : IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// The _table name
        /// </summary>
        private readonly string _tableName;
        private readonly string _conn;
        private string tableName;


        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_conn);

            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}" /> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        protected Repository(string tableName,string conn)
        {
            _tableName = tableName;
            _conn = conn;

        }

        public Repository(string tableName)
        {
            this.tableName = tableName;
        }

        /// <summary>
        /// Mapping the object to the insert/update columns.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The parameters with values.</returns>
        /// <remarks>In the default case, we take the object as is with no custom mapping.</remarks>
        internal virtual dynamic Mapping(T item)
        {
            return item;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Add(T item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                //item.Id = cn.Insert<string>(_tableName, parameters);
            }
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Update(T item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                cn.Update(_tableName, parameters);
            }
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Remove(T item)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                //cn.Execute("DELETE FROM " + _tableName + " WHERE Id=@Id", new { item.Id });
            }
        }

        /// <summary>
        /// Finds by Id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual T FindByID(string id)
        {
            T item = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                //item = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE Id=@Id", new { Id = id }).SingleOrDefault();
            }

            return item;
        }

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A list of items</returns>
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> items;

            // extract the dynamic sql query and parameters from predicate
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>(result.Sql, (object)result.Param);
            }

            return items;
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>All items</returns>
        public virtual IEnumerable<T> FindAll()
        {
            IEnumerable<T> items;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM " + _tableName);
            }

            return items;
        }
    }
}
