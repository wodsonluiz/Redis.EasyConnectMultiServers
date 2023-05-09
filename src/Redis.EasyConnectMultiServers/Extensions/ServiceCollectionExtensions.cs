using Redis.EasyConnectMultiServers.Abstractions;
using Redis.EasyConnectMultiServers.Implementations;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension of IServiceCollection for build IRedisProviderMultiServers
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Setup RedisClientFactory and RedisProviderMultiServers 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="redisConfiguration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisProviderMultiServers(this IServiceCollection services, IEnumerable<RedisConfiguration> redisConfiguration) 
        {
            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(redisConfiguration);
            services.AddSingleton<IRedisProviderMultiServers, RedisProviderMultiServers>();

            return services;
        }

        /// <summary>
        /// Setup RedisClientFactory and RedisProviderMultiServers 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="redisConfiguration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisProviderMultiServers(this IServiceCollection services, RedisConfiguration redisConfiguration) 
        {
            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(redisConfiguration);
            services.AddSingleton<IRedisProviderMultiServers, RedisProviderMultiServers>();

            return services;
        }

        /// <summary>
        /// Setup RedisClientFactory and RedisProviderMultiServers 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="redisConfigurationFactory"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisProviderMultiServers(this IServiceCollection services, Func<IServiceProvider, IEnumerable<RedisConfiguration>> redisConfigurationFactory) 
        {
            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(redisConfigurationFactory);
            services.AddSingleton<IRedisProviderMultiServers, RedisProviderMultiServers>();

            return services;
        }
    }
}