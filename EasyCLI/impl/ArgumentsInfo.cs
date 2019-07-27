using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyCli.Impl
{
    internal class ArgumentsInfo : IArgumentsInfo
    {
        // TODO: add additional parameter of configuration for custom splitting of arguments
        internal ArgumentsInfo(string arguments)
        {
            RawString = arguments;
            Arguments = SplitString(arguments);
            CleanString = CleanArgsString(Arguments);
        }

        public string RawString { get; }
        public string CleanString { get; }
        public List<string> Arguments { get; }

        public double? GetAsDouble(int index) => double.TryParse(Arguments[index], out double result) ? result : default(double?);
        public float? GetAsFloat(int index) => float.TryParse(Arguments[index], out float result) ? result : default(float?);
        public int? GetAsInt(int index) => int.TryParse(Arguments[index], out int result) ? result : default(int?);

        public T? GetAsStruct<T>(int index) where T : struct => throw new System.NotImplementedException();
        public T GetAsType<T>(int index) where T : class => throw new System.NotImplementedException();

        #region Private Helper Methods
        private string CleanArgsString(List<string> argumentList)
        {
            return string.Join(" ", argumentList);
        }

        private List<string> SplitString(string arguments)
        {
            // TODO: Support different splitInformations
            //  with a map of startString -> endString, preferably in a recursive form
            //  e.g. '{name:"John Doe",parents:{father:"Paul Doe",mother:"Jane Doe"}}'
            //  should be one section
            return arguments.Trim().Split(' ').Where(str => !string.IsNullOrWhiteSpace(str)).ToList();
        }
        #endregion
    }
}
