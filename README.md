# Redis.EasyConnectMultiServers
![Nuget](https://img.shields.io/nuget/dt/Redis.EasyConnectMultiServers)
![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/wodsonluiz/Redis.EasyConnectMultiServers/dotnet.yml)
![Nuget](https://img.shields.io/nuget/v/Redis.EasyConnectMultiServers)

Biblioteca para facilitar e realizar todas as operações de CRUD em multiplos servidores do Redis.

### Setting

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

services.AddRedisProviderMultiServers<SystemTextJsonSerializer>(configurations);
```

### Example using

```csharp
public class WeatherForecastController : ControllerBase
{
    private readonly IRedisProviderMultiServers _redisProviderMultiServers;

    public WeatherForecastController(IRedisProviderMultiServers redisProviderMultiServers)
    {
        _redisProviderMultiServers = redisProviderMultiServers;
    }
}

```

#### Add example

```csharp
await _redisProviderMultiServers.AddMultiAsync(Guid.NewGuid().ToString(), "value");
```

#### Get example
```csharp
await _redisProviderMultiServers.GetMultiAsync<string>(guid.ToString());
```