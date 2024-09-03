using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;
using VetHub02.Core.Specifications;

namespace VetHub02.Repository
{
    public static class SpecificationEvalutor<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inquery,ISpecification<TEntity> spec)
        {
            var outquery = inquery; //context.?      //ex: ? => article .inc(a =>  a.user)

            if(spec.Predicate is not null)
                    outquery = outquery.Where(spec.Predicate);

            if(spec.OrderBy is not null)
                    outquery = outquery.OrderBy(spec.OrderBy);

            if(spec.OrderByDescending is not null)
                    outquery = outquery.OrderByDescending(spec.OrderByDescending);

            if(spec.IsPaginationEnabled)
                    outquery = outquery.Skip(spec.Skip).Take(spec.Take);


            outquery = spec.Includes.Aggregate(outquery,(CurrentQuery,IncludeExpression)=> CurrentQuery.Include(IncludeExpression));

            return outquery;
        }
    }
}
