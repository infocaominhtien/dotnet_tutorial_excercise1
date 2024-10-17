namespace WebApplication7;

public static class ConfigHelper
{
    public static IConfiguration Config;

    public static void Init(IConfiguration config)
    {
        Config = config;
    }
}