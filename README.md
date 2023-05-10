# Redis.EasyConnectMultiServers 

![Nuget](https://img.shields.io/nuget/dt/Redis.EasyConnectMultiServers)
![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/wodsonluiz/Redis.EasyConnectMultiServers/dotnet.yml)
![Nuget](https://img.shields.io/nuget/v/Redis.EasyConnectMultiServers)

[PTBR Version](https://github.com/wodsonluiz/Redis.EasyConnectMultiServers/blob/main/README-PTB.md)

## Package for ease integration with multiple servers Redis. :package:

### Configuration
- `AddRedisProviderMultiServers`: Inject a singleton instance of `IRedisProviderMultiServers`.
- `IRedisProviderMultiServers` Abstraction responsible for provider contracts that operations of CRUD in multiple servers 

```csharp
var configurations = new[]
{
    new RedisConfiguration
    {
        AbortOnConnectFail = true,
        KeyPrefix = "MyPrefix__",
        Hosts = new[] { new RedisHost { Host = "localhost", Port = 6379 } },
        AllowAdmin = true,
        ConnectTimeout = 5000,
        Database = 0,
        PoolSize = 5,
        IsDefault = true
    },
    new RedisConfiguration
    {
        AbortOnConnectFail = true,
        KeyPrefix = "MyPrefix__",
        Hosts = new[] { new RedisHost { Host = "localhost", Port = 6389 } },
        AllowAdmin = true,
        ConnectTimeout = 5000,
        Database = 0,
        PoolSize = 2,
        Name = "Secndary Instance"
    }
};

services.AddRedisProviderMultiServers(configurations);
```

### Exemple of use

```csharp
public class WeatherForecastController
{
    private readonly IRedisProviderMultiServers _redisProviderMultiServers;

    public WeatherForecastController(IRedisProviderMultiServers redisProviderMultiServers) =>
        _redisProviderMultiServers = redisProviderMultiServers;
}
```

#### Write/ Replace
- Write in all servers configured

```csharp
await _redisProviderMultiServers.AddAsync(Guid.NewGuid().ToString(), "value");
```

```csharp
await _redisProviderMultiServers.ReplaceAsync(Guid.NewGuid().ToString(), "value");
```

- Write only in default server: `IsDefault = true`

```csharp
await _redisProviderMultiServers.AddDefaultClientAsync(Guid.NewGuid().ToString(), "value");
```

```csharp
await _redisProviderMultiServers.ReplaceDefaultClientAsync(Guid.NewGuid().ToString(), "value");
```

#### Get
- The get is performed from the _client default_: `IsDefault = true`.
- The method responds as soon as it finds the key, not continuing with the get on other servers.

```csharp
await _redisProviderMultiServers.GetAsync<string>(guid.ToString());
```

#### Remove

- Remove in all servers configured
- Remove only in default server: `IsDefault = true`

```csharp
await _redisProviderMultiServers.RemoveAsync(key);
```

```csharp
await _redisProviderMultiServers.RemoveDefaultClientAsync(key);
```
