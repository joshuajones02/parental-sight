namespace Microsoft.Extensions.Configuration
{
    using ParentalSight;

    public static class IConfigurationExtensions
    {
        public static T GetRequiredValue<T>(this IConfiguration config, string key, bool allowDefault = false)
        {
            var value = config.GetValue<string>(key);

            if (string.IsNullOrEmpty(value))
                throw new ApplicationSettingException(key);

            return config.GetValue<T>(key);
        }
    }
}