using System;
using System.Collections.Generic;
using Redis.EasyConnectMultiServers.Abstractions;
using Redis.EasyConnectMultiServers.Implementations;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisProviderMultiServers(this IServiceCollection services, 
        IEnumerable<RedisConfiguration> configurations) 
        {
            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(configurations);
            services.AddSingleton<IRedisProviderMultiServers, RedisProviderMultiServers>();

            return services;
        }

        public static IServiceCollection AddRedisProviderMultiServers(this IServiceCollection services, 
        RedisConfiguration configuration) 
        {
            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(configuration);
            services.AddSingleton<IRedisProviderMultiServers, RedisProviderMultiServers>();

            return services;
        }

        public static IServiceCollection AddRedisProviderMultiServers(this IServiceCollection services, Func<IServiceProvider, IEnumerable<RedisConfiguration>> redisConfigurationFactory) 
        {
            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(redisConfigurationFactory);
            services.AddSingleton<IRedisProviderMultiServers, RedisProviderMultiServers>();

            return services;
        }
    }
}