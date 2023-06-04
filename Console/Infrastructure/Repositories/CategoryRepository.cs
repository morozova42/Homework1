using ConsoleApplication.Abstractions;
using Domain;
using Infrastructure;

namespace ConsoleApplication.Infrastructure.Repositories
{
	internal class CategoryRepository : IRepository<Category>
	{
		private ApplicationContext _ctx;
		public CategoryRepository(ApplicationContext ctx)
		{
			_ctx = ctx;
		}

		public bool Create(Category entity)
		{
			_ctx.Categories.Add(entity);
			return _ctx.SaveChanges() > 0;
		}

		public Category Get(int id)
		{
			return _ctx.Categories.FirstOrDefault(x => x.Id == id);
		}

		public IEnumerable<Category> GetAll()
		{
			return _ctx.Categories.ToList();
		}
	}
}
