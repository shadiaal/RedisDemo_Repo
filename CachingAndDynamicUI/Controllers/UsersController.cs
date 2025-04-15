using CachingAndDynamicUI.Models;
using CachingAndDynamicUI;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisCachingDemo.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly HttpClient _httpClient;
		private readonly IDatabase _cache;

		public UserController()
		{
			_httpClient = new HttpClient();
			_cache = RedisConnectorHelper.Connection.GetDatabase();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			string cacheKey = $"user:{id}";
			var cachedUser = await _cache.StringGetAsync(cacheKey);

			if (cachedUser.HasValue)
			{
				var user = JsonSerializer.Deserialize<User>(cachedUser);
				return Ok(user);
			}

			
			Thread.Sleep(5000);
			var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");
			if (!response.IsSuccessStatusCode) return NotFound();

			var json = await response.Content.ReadAsStringAsync();
			var userFromApi = JsonSerializer.Deserialize<User>(json);

			var serialized = JsonSerializer.Serialize(userFromApi);
			await _cache.StringSetAsync(cacheKey, serialized, expiry: System.TimeSpan.FromMinutes(5));

			return Ok(userFromApi);
		}
	}
}
