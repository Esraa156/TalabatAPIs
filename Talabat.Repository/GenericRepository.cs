﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
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
		public async Task<T?> Get(int id)
		{
			return await _dbcontext.Set<T>().FindAsync(id);
		}

		public async Task< IEnumerable<T>> GetAllAsync()
		{
			return await _dbcontext.Set<T>().ToListAsync();
		}
	}
}