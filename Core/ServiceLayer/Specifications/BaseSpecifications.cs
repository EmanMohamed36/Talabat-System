using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Specifications
{
    internal abstract class BaseSpecifications<TEntity, TKey>
        : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity,object>> OrderBY { get; private set; }
        public Expression<Func<TEntity, object>> OrderBYDescending { get; private set; }


        protected void AddInclude(Expression<Func<TEntity, object>> includeExpressions)
        {
            IncludeExpressions.Add(includeExpressions);
        }
        protected void AddOrderBY(Expression<Func<TEntity, object>> OrderBYExpressions)
        {
            OrderBY = OrderBYExpressions;
        }
        protected void AddOrderBYDescending(Expression<Func<TEntity, object>> OrderBYDescendingExpressions)
        {
            OrderBYDescending = OrderBYDescendingExpressions;
        }
    }
}
