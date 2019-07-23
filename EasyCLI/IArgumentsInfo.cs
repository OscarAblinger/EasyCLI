using System.Collections.Generic;

namespace EasyCli
{
    public interface IArgumentsInfo
    {
        /// <summary>
        /// The input string without any processing done
        /// </summary>
        string RawString { get; }

        /// <summary>
        /// The clean input string with uniform whitespaces
        /// </summary>
        string CleanString { get; }

        /// <summary>
        /// List of all arguments. 0th is the command name itself
        /// </summary>
        List<string> Arguments { get; }

        int? GetAsInt(int index);
        double? GetAsDouble(int index);
        float? GetAsFloat(int index);

        T GetAsType<T>(int index) where T : class;
        T? GetAsStruct<T>(int index) where T : struct;
    }
}
