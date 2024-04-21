﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbcontext;
		public GenericRepository(StoreContext dbcontext)
		{
			_dbcontext = dbcontext;
		}
		public async Task<T?> GetAsync(int id)
		{
			//if (typeof(T) == typeof(Product))
			//	return await _dbcontext.Set<Product>().Where(P => P.Id == id).Include(p => p.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;
			return await _dbcontext.Set<T>().FindAsync(id);


		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			//if (typeof(T) == typeof(Product))
			//	return (IEnumerable<T>)await _dbcontext.Set<Product>().Include(p => p.Brand).Include(P => P.Category).ToListAsync();
			return await _dbcontext.Set<T>().AsNoTracking().ToListAsync();

		}

		public async Task<IReadOnlyList<T>> GetAllAsyncSpec(ISpecification<T> spec)
		{
			return await ApplySpecifications( spec).AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetAsyncSpec(ISpecification<T> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
		}
		private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
		{
			return SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spec);
		}
		public async Task<int> GetCountAsync(ISpecification<T> spec)
		{
			return await ApplySpecifications(spec).CountAsync();
		}
	}
}