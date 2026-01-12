using StackExchange.Redis;
using System;

public static class RedisManager
{
    private static readonly Lazy<ConnectionMultiplexer> _redis =
        new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect("localhost:6379"));

    public static IDatabase Db => _redis.Value.GetDatabase();
}
