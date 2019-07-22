using EasyCli;

namespace EasyCli
{
    public static class CliFactory
    {
        public static ICli Create()
        {
            return new Cli();
        }
    }
}
