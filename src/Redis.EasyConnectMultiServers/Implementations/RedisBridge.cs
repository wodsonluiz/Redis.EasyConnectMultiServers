using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Redis.EasyConnectMultiServers.Implementations
{
    internal class RedisBridge
    {
        protected async Task<bool> AddAsync<T>(IRedisDatabase database, string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null) =>
            await database.AddAsync<T>(key, value, when, flag, tags);

        protected async Task<bool> AddAsync<T>(IRedisDatabase database, string key, T value, TimeSpan expiresIn, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null) =>
            await database.AddAsync<T>(key, value, expiresIn, when, flag, tags);

        protected async Task<T?> GetAsync<T>(IRedisDatabase database, string key, CommandFlags flag = CommandFlags.None) =>
             await database.GetAsync<T>(key, flag) ?? default;
        protected async Task<bool> RemoveAsync(IRedisDatabase database, string key, CommandFlags flags = CommandFlags.None) =>
             await database.RemoveAsync(key, flags);

        protected async Task<bool> ReplaceAsync<T>(IRedisDatabase database, string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None) =>
            await database.ReplaceAsync<T>(key, value, when, flag);

        protected async Task<bool> ReplaceAsync<T>(IRedisDatabase database, string key, T value, DateTimeOffset expiresAt, When when = When.Always, CommandFlags flag = CommandFlags.None) =>
            await database.ReplaceAsync<T>(key, value, expiresAt, when, flag);
    }
}