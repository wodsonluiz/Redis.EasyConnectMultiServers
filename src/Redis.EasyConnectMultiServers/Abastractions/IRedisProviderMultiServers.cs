using StackExchange.Redis;

namespace Redis.EasyConnectMultiServers.Abstractions
{
    /// <summary>
    /// Abstraction to provide methods with multiple servers
    /// </summary>
    public interface IRedisProviderMultiServers
    {
         /// <summary>
         /// Write in multi databases
         /// </summary>
         Task<bool> AddMultiAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null);

         /// <summary>
         /// Write in multi databases
         /// </summary>
         Task<bool> AddMultiAsync<T>(string key, T value, TimeSpan expiresIn, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null);

         /// <summary>
         /// Write in default database
         /// </summary>
         Task<bool> AddDefaultAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null);

         /// <summary>
         /// Write in default database
         /// </summary>
         Task<bool> AddDefaultAsync<T>(string key, T value, TimeSpan expiresIn, When when = When.Always, CommandFlags flag = CommandFlags.None, HashSet<string>? tags = null);

         /// <summary>
         /// Replace in multi databases
         /// </summary>
         Task<bool> ReplaceMultiAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None);

         /// <summary>
         /// Replace in multi databases
         /// </summary>
         Task<bool> ReplaceMultiAsync<T>(string key, T value, DateTimeOffset expiresAt, When when = When.Always, CommandFlags flag = CommandFlags.None);

         /// <summary>
         /// Replace in default database
         /// </summary>
         Task<bool> ReplaceDefaultAsync<T>(string key, T value, When when = When.Always, CommandFlags flag = CommandFlags.None);

         /// <summary>
         /// Replace in default database
         /// </summary>
         Task<bool> ReplaceDefaultAsync<T>(string key, T value, DateTimeOffset expiresAt, When when = When.Always, CommandFlags flag = CommandFlags.None);

         /// <summary>
         /// Replace in default database
         /// </summary>
         Task<bool> RemoveMultiAsync(string key, CommandFlags flags = CommandFlags.None);

         /// <summary>
         /// Remove in default database
         /// </summary>
         Task<bool> RemoveDefaultAsync(string key, CommandFlags flags = CommandFlags.None);

         /// <summary>
         /// Get in multi databases, first read default database
         /// </summary>
         Task<T> GetMultiAsync<T>(string key, CommandFlags flag = CommandFlags.None);

        /// <summary>
         /// Get in default database
         /// </summary>
         Task<T> GetDefaultAsync<T>(string key, CommandFlags flag = CommandFlags.None);
    
    }
}