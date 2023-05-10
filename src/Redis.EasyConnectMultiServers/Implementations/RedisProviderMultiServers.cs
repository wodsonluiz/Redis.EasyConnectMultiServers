using Redis.EasyConnectMultiServers.Abstractions;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Redis.EasyConnectMultiServers.Implementations
{
    internal class RedisProviderMultiServers : RedisBridge, IRedisProviderMultiServers
    {
        private readonly IRedisDatabase _databaseDefault;
        private readonly IEnumerable<IRedisClient> _redisClients;

        public RedisProviderMultiServers(IRedisClientFactory factory)
        {
            _redisClients = factory.GetAllClients();
            _databaseDefault = factory.GetDefaultRedisClient().GetDefaultDatabase();
        }

        public Task<bool> AddDefaultClientAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null) =>
            AddAsync<T>(_databaseDefault, key, value, when, flag, tags);


        public Task<bool> AddDefaultClientAsync<T>(string key, T value, TimeSpan expiresIn, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null) =>
             AddAsync<T>(_databaseDefault, key, value, expiresIn, when, flag, tags);

        public async Task<bool> AddAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null)
        {
            var listTasks = new List<Task<bool>>();

            foreach (var client in _redisClients)
            {
                var result = base.AddAsync(client.GetDefaultDatabase(), key, value, when, flag, tags);

                listTasks.Add(result);
            }

            await Task.WhenAll(listTasks);

            return listTasks.Any() && listTasks.All(x => x.Result);
        }

        public async Task<bool> AddAsync<T>(string key, T value, TimeSpan expiresIn, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null)
        {
            var listTasks = new List<Task<bool>>();

            foreach (var client in _redisClients)
            {
                var result = AddAsync(client.GetDefaultDatabase(), key, value, expiresIn, when, flag, tags);

                listTasks.Add(result);
            }

            await Task.WhenAll(listTasks);

            return listTasks.Any() && listTasks.All(x => x.Result);
        }

        public async Task<T> GetDefaultClientAsync<T>(string key, CommandFlags flag = CommandFlags.None) =>
            await GetAsync<T>(_databaseDefault, key, flag) ?? default!;

        public async Task<T> GetAsync<T>(string key, CommandFlags flag = CommandFlags.None)
        {
            foreach (var client in _redisClients)
            {
                var resultTask = await GetAsync<T>(client.GetDefaultDatabase(), key);

                if (resultTask != null)
                    return resultTask;
            }

            return default!;
        }

        public Task<bool> RemoveDefaultClientAsync(string key, CommandFlags flags = CommandFlags.None) =>
            RemoveAsync(_databaseDefault, key, flags);

        public async Task<bool> RemoveAsync(string key, CommandFlags flags = CommandFlags.None)
        {
            var listTaks = new List<Task<bool>>();

            foreach (var client in _redisClients)
            {
                var result = RemoveAsync(client.GetDefaultDatabase(), key);

                listTaks.Add(result);
            }

            await Task.WhenAll(listTaks);

            return listTaks.Any() && listTaks.All(x => x.Result);
        }

        public Task<bool> ReplaceDefaultClientAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None) =>
            ReplaceAsync<T>(_databaseDefault, key, value, when, flag);

        public Task<bool> ReplaceDefaultClientAsync<T>(string key, T value, DateTimeOffset expiresAt, When when = When.Always, CommandFlags flag = CommandFlags.None) =>
            ReplaceAsync<T>(_databaseDefault, key, value, expiresAt, when, flag);

        public async Task<bool> ReplaceAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None)
        {
            var listTaks = new List<Task<bool>>();

            foreach (var client in _redisClients)
            {
                var result = ReplaceAsync<T>(client.GetDefaultDatabase(), key, value, when, flag);

                listTaks.Add(result);
            }

            await Task.WhenAll(listTaks);

            return listTaks.Any() && listTaks.All(x => x.Result);
        }

        public async Task<bool> ReplaceAsync<T>(string key, T value, DateTimeOffset expiresAt, When when = When.Always, CommandFlags flag = CommandFlags.None)
        {
            var listTaks = new List<Task<bool>>();

            foreach (var client in _redisClients)
            {
                var result = ReplaceAsync<T>(client.GetDefaultDatabase(), key, value, expiresAt, when, flag);

                listTaks.Add(result);
            }

            await Task.WhenAll(listTaks);

            return listTaks.Any() && listTaks.All(x => x.Result);
        }
    }
}