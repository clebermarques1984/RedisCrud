using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisCrud
{
	public class RedisKeyValueStore : IKeyValueStore
	{
		private readonly IDatabase store;

		public RedisKeyValueStore()
		{
		}

		public RedisKeyValueStore(IDatabase store)
		{
			this.store = store;
		}

		public void Add<T>(string key, T type)
		{
			var db = GetRedisConn();
			string json = JsonConvert.SerializeObject(type, Formatting.Indented);
			db.StringSet(key, json);
		}

		public T GetById<T>(string key)
		{
			var db = GetRedisConn();
			var json = db.StringGet(key);
			return JsonConvert.DeserializeObject<T>(json);
		}

		public IEnumerable<T> GetAll<T>()
		{
			var db = GetRedisConn();
			var endpoints = db.Multiplexer.GetEndPoints();
			var server = db.Multiplexer.GetServer(endpoints.First());

			return server.Keys().Select(k => GetById<T>(k));
		}

		public void Delete(string key)
		{
			var db = GetRedisConn();
			db.KeyDelete(key);
		}

		private IDatabase GetRedisConn()
		{
			return store ?? ConnectionMultiplexer.Connect("localhost").GetDatabase();
		}
	}
}
