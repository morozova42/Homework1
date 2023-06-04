using ConsoleApplication.Abstractions;
using Domain;
using Infrastructure;

namespace ConsoleApplication.Infrastructure.Repositories
{
	internal class UserRepository : IRepository<User>
	{
		private ApplicationContext _ctx;
		public UserRepository(ApplicationContext ctx)
		{
			_ctx = ctx;
		}

		public bool Create(User entity)
		{
			_ctx.Users.Add(entity);
			return _ctx.SaveChanges() > 0;
		}

		User IRepository<User>.Get(int id)
		{
			return _ctx.Users.FirstOrDefault(x => x.Id == id);
		}

		IEnumerable<User> IRepository<User>.GetAll()
		{
			return _ctx.Users.ToList();
		}
	}
}
