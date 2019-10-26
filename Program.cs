using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisCrud
{
	class Program
	{
		static void Main(string[] args)
		{
			IKeyValueStore db = new RedisKeyValueStore();

			var p1 = new Person() { ID = 1, Name = "Cleber" };
			var p2 = new Person() { ID = 2, Name = "Marques" };
			var p3 = new Person() { ID = 3, Name = "Ferreira" };

			db.Add(p1.ID.ToString(), p1);
			db.Add(p2.ID.ToString(), p2);
			db.Add(p3.ID.ToString(), p3);

			Console.WriteLine("Show 1, 2, 3");
			foreach (var item in db.GetAll<Person>())
				Console.WriteLine($"id: {item.ID} - Name: {item.Name}");

			Console.WriteLine("\nDelete 1\n");
			db.Delete(p1.ID.ToString());

			Console.WriteLine("Show 2, 3");
			foreach (var item in db.GetAll<Person>())
				Console.WriteLine($"id: {item.ID} - Name: {item.Name}");

			Console.ReadKey();
		}
	}
}
