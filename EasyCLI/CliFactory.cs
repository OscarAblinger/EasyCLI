using EasyCli.impl;

namespace EasyCli
{
    public static class CliFactory
    {
        public static ICli Create() => new Cli(CreateDefaultConfig());

        public static IConfiguration CreateDefaultConfig() => new Configuration();
    }
}
