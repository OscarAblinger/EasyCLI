using EasyCli.Impl;
using System;
using System.Collections.Generic;

namespace EasyCli
{
    public static class CliFactory
    {
        public static ICli Create()
        {
            return new Cli(CreateDefaultConfig());
        }

        public static IConfiguration CreateDefaultConfig()
        {
            return new Configuration();
        }
    }
}
