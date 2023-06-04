using ConsoleApplication.Abstractions;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApplication.Infrastructure.Repositories
{
	internal class AdvertRepository : IRepository<Advert>
	{
		private ApplicationContext _ctx;
		public AdvertRepository(ApplicationContext ctx)
		{
			_ctx = ctx;
		}

		public bool Create(Advert entity)
		{
			_ctx.Adverts.Add(entity);
			return _ctx.SaveChanges() > 0;
		}

		public Advert Get(int id)
		{
			return _ctx.Adverts.FirstOrDefault(x => x.Id == id);
		}

		public IEnumerable<Advert> GetAll()
		{
			return _ctx.Adverts
				.Include(adv => adv.User)
				.Include(adv => adv.Category)
				.ToList();
		}
	}
}
