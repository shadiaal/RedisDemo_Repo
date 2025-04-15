using CachingAndDynamicUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace CachingAndDynamicUI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserMemoryController : ControllerBase
	{
		private readonly HttpClient _httpClient;
		private readonly IMemoryCache _cache;

		public UserMemoryController(IMemoryCache cache)
		{
			_httpClient = new HttpClient();
			_cache = cache;
		}

		// GET api/User/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			string cacheKey = $"user:{id}";

			// Try to get the user from memory cache
			if (!_cache.TryGetValue(cacheKey, out User cachedUser))
			{
				
				var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");
				if (!response.IsSuccessStatusCode)
				{
					return NotFound(); 
				}
				Thread.Sleep(5000);
				var json = await response.Content.ReadAsStringAsync();
				var userFromApi = JsonSerializer.Deserialize<User>(json);

				// Cache the user data for 5 minutes
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromMinutes(5)); 

				_cache.Set(cacheKey, userFromApi, cacheEntryOptions);

				return Ok(userFromApi); 
			}

			
			return Ok(cachedUser); // Return the user from memory cache
		}
	}
}
