using System;

namespace EasyCli
{
    public class Either<T1, T2>
    {
        public Either(T1 first)
        {
            First = first;
            IsFirst = true;
        }

        public Either(T2 second)
        {
            Second = second;
            IsFirst = false;
        }

        public T1 First { get; }
        public T2 Second { get; }
        public bool IsFirst { get; }

        public T Match<T>(Func<T1, T> firstTransform, Func<T2, T> secondTransform)
        {
            return IsFirst ? firstTransform(First) : secondTransform(Second);
        }

        public static implicit operator Either<T1, T2>(T1 first)
        {
            return new Either<T1, T2>(first);
        }

        public static implicit operator Either<T1, T2>(T2 second)
        {
            return new Either<T1, T2>(second);
        }
    }
}
