using System.Collections.Generic;

namespace EasyCli
{
    public interface IArgumentsInfo
    {
        string RawString { get; }
        string CommandString { get; }
        List<string> Arguments { get; }
        List<string> Options { get; }

        int? GetAsInt(int index);
        double? GetAsDouble(int index);
        float? GetAsFloat(int index);

        T GetAsType<T>(int index) where T : class;
        T? GetAsStruct<T>(int index) where T : struct;
    }
}
