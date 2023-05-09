using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redis.EasyConnectMultiServers.Abstractions;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Redis.EasyConnectMultiServers.Implementations
{
    public class RedisProviderMultiServers : RedisBridge, IRedisProviderMultiServers
    {
        private readonly IRedisClientFactory _factory;
        private readonly IRedisDatabase _databaseDefault;
        private readonly IRedisClient _redisClientDefault;
        private readonly IEnumerable<IRedisClient> _redisClients;

        public RedisProviderMultiServers(IRedisClientFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException($" {nameof(factory)} is null");
            _redisClients = factory.GetAllClients();
            _redisClientDefault = factory.GetDefaultRedisClient();
            _databaseDefault = factory.GetDefaultRedisClient().GetDefaultDatabase();
        }
        
        public Task<bool> AddDefaultAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string> tags = null) =>
            base.AddAsync<T>(_databaseDefault, key, value, when, flag, tags);
        

        public Task<bool> AddDefaultAsync<T>(string key, T value, TimeSpan expiresIn, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string> tags = null) =>
             base.AddAsync<T>(_databaseDefault, key, value, expiresIn, when, flag, tags);

        public async Task<bool> AddMultiAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string> tags = null)
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

        public async Task<bool> AddMultiAsync<T>(string key, T value, TimeSpan expiresIn, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string> tags = null)
        {
            var listTasks = new List<Task<bool>>();

            foreach (var client in _redisClients)
            {
                var result = base.AddAsync(client.GetDefaultDatabase(), key, value, expiresIn, when, flag, tags);

                listTasks.Add(result);
            }

            await Task.WhenAll(listTasks);

            return listTasks.Any() && listTasks.All(x => x.Result);
        }

        public Task<T> GetDefaultAsync<T>(string key, CommandFlags flag = CommandFlags.None) =>
            base.GetAsync<T>(_databaseDefault, key, flag);

        public async Task<T> GetMultiAsync<T>(string key, CommandFlags flag = CommandFlags.None)
        {
            foreach (var client in _redisClients)
            {
                var resultTask = await base.GetAsync<T>(client.GetDefaultDatabase(), key);

                if (resultTask != null)
                    return resultTask;
            }

            return default(T);
        }

        public Task<bool> RemoveDefaultAsync(string key, CommandFlags flags = CommandFlags.None) =>
            base.RemoveAsync(_databaseDefault, key, flags); 

        public async Task<bool> RemoveMultiAsync(string key, CommandFlags flags = CommandFlags.None)
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

        public Task<bool> ReplaceDefaultAsync<T>(string key, 
         T value, 
         When when = When.Always, 
         CommandFlags flag = CommandFlags.None)
        {
            return base.ReplaceAsync<T>(_databaseDefault, key, value, when, flag);
        }

        public Task<bool> ReplaceDefaultAsync<T>(string key, T value, DateTimeOffset expiresAt, When when = When.Always, CommandFlags flag = CommandFlags.None) =>
            base.ReplaceAsync<T>(_databaseDefault, key, value, expiresAt, when, flag);

        public async Task<bool> ReplaceMultiAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None)
        {
            var listTaks = new List<Task<bool>>();

            foreach (var client in _redisClients)
            {
                var result = base.ReplaceAsync<T>(client.GetDefaultDatabase(), key, value, when, flag);

                listTaks.Add(result);
            }

            await Task.WhenAll(listTaks);

            return listTaks.Any() && listTaks.All(x => x.Result);
        }

        public async Task<bool> ReplaceMultiAsync<T>(string key, T value, DateTimeOffset expiresAt, When when = When.Always, CommandFlags flag = CommandFlags.None)
        {
            var listTaks = new List<Task<bool>>();

            foreach (var client in _redisClients)
            {
                var result = base.ReplaceAsync<T>(client.GetDefaultDatabase(), key, value, expiresAt, when, flag);

                listTaks.Add(result);
            }

            await Task.WhenAll(listTaks);

            return listTaks.Any() && listTaks.All(x => x.Result);
        }
    }
}