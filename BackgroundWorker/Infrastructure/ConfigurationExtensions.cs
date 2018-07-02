using Microsoft.Extensions.Configuration;

namespace BackgroundWorker.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static T ParseAs<T>(this IConfiguration configuration, string path)
            where T : new()
        {
            var result = new T();
            configuration.GetSection(path).Bind(result);
            return result;
        }
    }
}
