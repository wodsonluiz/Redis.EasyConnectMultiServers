using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Redis.EasyConnectMultiServers.Implementations
{
    internal class RedisBridge
    {
        protected Task<bool> AddAsync<T>(IRedisDatabase database, string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null) =>
            database.AddAsync(key, value, when, flag, tags);

        protected Task<bool> AddAsync<T>(IRedisDatabase database, string key, T value, TimeSpan expiresIn, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null) =>
            database.AddAsync(key, value, expiresIn, when, flag, tags);

        protected Task<T?> GetAsync<T>(IRedisDatabase database, string key, CommandFlags flag = CommandFlags.None) =>
            database.GetAsync<T?>(key, flag);
             
        protected Task<bool> RemoveAsync(IRedisDatabase database, string key, CommandFlags flags = CommandFlags.None) =>
            database.RemoveAsync(key, flags);

        protected Task<bool> ReplaceAsync<T>(IRedisDatabase database, string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None) =>
            database.ReplaceAsync(key, value, when, flag);

        protected Task<bool> ReplaceAsync<T>(IRedisDatabase database, string key, T value, DateTimeOffset expiresAt, When when = When.Always, CommandFlags flag = CommandFlags.None) =>
            database.ReplaceAsync(key, value, expiresAt, when, flag);
    }
}