using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Predicate { get; set; } = null!;
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderBy { get ; set ; } = null!;
        public Expression<Func<T, object>> OrderByDescending { get ; set ; } = null !;
        public int Skip { get ; set ; }
        public int Take { get; set ; }
        public bool IsPaginationEnabled { get ; set; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>> Predicate)
        {
            this.Predicate = Predicate;          
        }

        public void AddOrderBy(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy;
        }
        public void AddOrderByDescndein(Expression<Func<T, object>> OrderByDescending)
        {
            this.OrderByDescending = OrderByDescending;
        } 

        public void ApplyPagination(int skip , int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
