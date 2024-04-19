using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	public static class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
	{

		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity>InputQuery,ISpecification<TEntity>Spec)
		{
			var Query = InputQuery;//_dbcontext.Set<TEntity>();

			if(Spec.Criteria is not null)
			{
				Query = Query.Where(Spec.Criteria);
				//_dbcontext.Set<TEntity>().where(Criteria);
			}
			if(Spec.OrderBy is not null)
			{
				Query= Query.OrderBy(Spec.OrderBy);
			}
			else if (Spec.OrderByDesc is not null)
			{
				Query=Query.OrderByDescending(Spec.OrderByDesc);	
			}

			Query = Spec.Includes.Aggregate(Query, (CurrentQuery, includeExpression) => CurrentQuery.Include(includeExpression));
			return Query;
		}

	}
}
