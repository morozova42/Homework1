using ConsoleApplication.Abstractions;
using Infrastructure;

namespace ConsoleApplication.Infrastructure
{
	internal class EFRepository<T> : IRepository<T> where T : BaseEntity
	{
		private ApplicationContext _ctx;
		public EFRepository()
		{
			_ctx = new ApplicationContext();
		}

		public bool Create(T entity)
		{
			_ctx.Set<T>().Add(entity);
			return _ctx.SaveChanges() > 0;
		}

		public T Get(int id)
		{
			return _ctx.Set<T>().FirstOrDefault(x => x.Id == id);
		}

		public IEnumerable<T> GetAll()
		{
			return _ctx.Set<T>().ToList();
		}
	}
}
