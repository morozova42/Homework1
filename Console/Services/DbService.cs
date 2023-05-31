using ConsoleApplication.Abstractions;
using Domain;
using Infrastructure;
using System.Text;

namespace Services
{
	public class DbService
	{
		private IRepository<User> _userRepository;
		private IRepository<Advert> _advertRepository;
		private IRepository<Category> _categoryRepository;

		public DbService(IRepository<User> userRepository,
			IRepository<Advert> advertRepository,
			IRepository<Category> categoryRepository)
		{
			_userRepository = userRepository;
			_advertRepository = advertRepository;
			_categoryRepository = categoryRepository;
		}

		#region Adverts methods
		public IEnumerable<Advert> GetAllAdverts()
		{
			return _advertRepository.GetAll();
		}
		#endregion
		public IEnumerable<Category> GetAllCategories()
		{
			return _categoryRepository.GetAll();
		}

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

		//public bool CreateAdvert(Dictionary<string, string> propDict)
		//{
		//	Advert newAdvert = new Advert
		//	{
		//		Category = _categoryRepository.Get propDict[nameof(Advert.Category)],
		//	};
		//	return _advertRepository.Create(newAdvert);
		//}
	}
}
