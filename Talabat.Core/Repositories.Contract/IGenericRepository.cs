using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
	public interface IGenericRepository<T> where T : BaseEntity
	{

		Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetAsync(int id);


	}
}
