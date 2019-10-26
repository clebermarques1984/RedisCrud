using System.Collections.Generic;

namespace RedisCrud
{
	public interface IKeyValueStore
	{
		void Add<T>(string key, T type);
		void Delete(string key);
		IEnumerable<T> GetAll<T>();
		T GetById<T>(string key);
	}
}