# Redis.EasyConnectMultiServers
![Nuget](https://img.shields.io/nuget/dt/Redis.EasyConnectMultiServers)
![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/wodsonluiz/Redis.EasyConnectMultiServers/dotnet.yml)
![Nuget](https://img.shields.io/nuget/v/Redis.EasyConnectMultiServers)

Biblioteca para facilitar a integração com multiplos servidores do Redis, assim sendo fácil realizar todas as operações de crud.

### Configuração
- `AddRedisProviderMultiServers`: Injeta uma instância singleton de `IRedisProviderMultiServers`.
- `IRedisProviderMultiServers` Abstração responsável por fornecer contratos que realiza as operações de CRUD em multiplos servidores

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

### Exemplo de uso

```csharp
public class WeatherForecastController
{
    private readonly IRedisProviderMultiServers _redisProviderMultiServers;

    public WeatherForecastController(IRedisProviderMultiServers redisProviderMultiServers) =>
        _redisProviderMultiServers = redisProviderMultiServers;
}
```

#### Escrever/ Atualizar
- Vai escrever em todos os sevidores configurados.

```csharp
await _redisProviderMultiServers.AddAsync(Guid.NewGuid().ToString(), "value");
```

```csharp
await _redisProviderMultiServers.ReplaceAsync(Guid.NewGuid().ToString(), "value");
```

- Vai escrever somente no servidor padrão: `IsDefault = true`

```csharp
await _redisProviderMultiServers.AddDefaultClientAsync(Guid.NewGuid().ToString(), "value");
```

```csharp
await _redisProviderMultiServers.ReplaceDefaultClientAsync(Guid.NewGuid().ToString(), "value");
```

#### Consulta
- A consulta é realizada a partir do _client default_: `IsDefault = true`.
- O método responde assim que encontra a chave, não seguindo com a consulta nos demais servidores. 

```csharp
await _redisProviderMultiServers.GetMultiAsync<string>(guid.ToString());
```

#### Remover

- Vai remover em todos os servidores configurados
- Vai remover somente no cliente padrão: `IsDefault = true`

```csharp
await _redisProviderMultiServers.RemoveAsync(key);
```

```csharp
await _redisProviderMultiServers.RemoveDefaultClientAsync(key);
```
