using Core.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data.Core.Repositories.Base
{
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Add(T item);

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Remove(T item);

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Update(T item);

        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T FindByID(string id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();
    }
}