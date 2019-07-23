using System.Collections.Generic;

namespace EasyCli.impl
{
    internal class ArgumentsInfo : IArgumentsInfo
    {
        public string RawString { get; }
        public string CommandString { get; }
        public List<string> Arguments { get; }
        public List<string> Options { get; }

        public double? GetAsDouble(int index) => double.TryParse(Arguments[index], out double result) ? result : default(double?);
        public float? GetAsFloat(int index) => float.TryParse(Arguments[index], out float result) ? result : default(float?);
        public int? GetAsInt(int index) => int.TryParse(Arguments[index], out int result) ? result : default(int?);

        public T? GetAsStruct<T>(int index) where T : struct => throw new System.NotImplementedException();
        public T GetAsType<T>(int index) where T : class => throw new System.NotImplementedException();
    }
}
