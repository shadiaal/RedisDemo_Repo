﻿using StackExchange.Redis;

namespace CachingAndDynamicUI
{
	public class RedisConnectorHelper
	{
		static RedisConnectorHelper()
		{
			RedisConnectorHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
			{
				return ConnectionMultiplexer.Connect("localhost");
			});
		}

		private static Lazy<ConnectionMultiplexer> lazyConnection;

		public static ConnectionMultiplexer Connection
		{
			get
			{
				return lazyConnection.Value;
			}
		}
	}
}

	
