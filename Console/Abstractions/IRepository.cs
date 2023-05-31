namespace ConsoleApplication.Abstractions
{
	public interface IRepository<T> where T : BaseEntity
	{
		public IEnumerable<T> GetAll();
		public T Get(int id);
		public bool Create(T entity);
	}
}
