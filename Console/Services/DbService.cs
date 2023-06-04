using ConsoleApplication.Abstractions;
using ConsoleApplication.Infrastructure.Repositories;
using Domain;
using Infrastructure;

namespace Services
{
	/// <summary>
	/// Сервис работы с репозиториями
	/// </summary>
	public class DbService
	{
		private ApplicationContext _ctx = new ApplicationContext();
		private IRepository<User> _userRepository;
		private IRepository<Advert> _advertRepository;
		private IRepository<Category> _categoryRepository;
		private bool disposed = false;

		public DbService()
		{
			_userRepository = new UserRepository(_ctx);
			_advertRepository = new AdvertRepository(_ctx);
			_categoryRepository = new CategoryRepository(_ctx);
		}

		#region Advert methods
		public IEnumerable<Advert> GetAllAdverts()
		{
			return _advertRepository.GetAll();
		}
		#endregion

		#region Category methods
		public IEnumerable<Category> GetAllCategories()
		{
			return _categoryRepository.GetAll();
		}

		public bool CreateCategory(Dictionary<string, string> propDict)
		{
			Category newCategory = new Category
			{
				Title = propDict[nameof(Category.Title)],
				Description = propDict[nameof(Category.Description)]
			};
			return _categoryRepository.Create(newCategory);
		}
		#endregion

		#region User methods
		public IEnumerable<User> GetAllUsers()
		{
			return _userRepository.GetAll();
		}

		public bool CreateUser(Dictionary<string, string> propDict)
		{
			User newUser = new User
			{
				LastName = propDict[nameof(User.LastName)],
				FirstName = propDict[nameof(User.FirstName)],
				MiddleName = propDict[nameof(User.MiddleName)]
			};
			return _userRepository.Create(newUser);
		}
		#endregion

		public void Dispose()
		{
			if (!disposed)
			{
				_ctx.Dispose();
			}
			disposed = true;
			GC.SuppressFinalize(this);
		}
	}
}
