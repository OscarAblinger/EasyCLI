using EasyCli.impl;

namespace EasyCli
{
    public static class CliFactory
    {
        public static ICli Create() => new Cli();
    }
}
