using System;
using System.Collections.Generic;
using static EasyCli.OneOf;

namespace EasyCli
{
    public class OneOf
    {
        public enum WhichType
        {
            first, second, third
        }
    }

    public class OneOf<T1, T2, T3>
    {
        public OneOf(T1 first)
        {
            First = first;
            FilledType = WhichType.first;
        }

        public OneOf(T2 second)
        {
            Second = second;
            FilledType = WhichType.second;
        }

        public OneOf(T3 third)
        {
            Third = third;
            FilledType = WhichType.third;
        }

        public T1 First { get; }
        public T2 Second { get; }
        public T3 Third { get; }
        public WhichType FilledType { get; }

        public T Match<T>(Func<T1, T> firstTransform, Func<T2, T> secondTransform, Func<T3, T> thirdTransform)
        {
            switch (FilledType)
            {
                case WhichType.first:
                    return firstTransform(First);
                case WhichType.second:
                    return secondTransform(Second);
                case WhichType.third:
                    return thirdTransform(Third);
                default:
                    throw new ArgumentException(); // really should be impossible
            }
        }

        public void Match(Action<T1> firstTransform, Action<T2> secondTransform, Action<T3> thirdTransform)
        {
            switch (FilledType)
            {
                case WhichType.first:
                    firstTransform(First);
                    break;
                case WhichType.second:
                    secondTransform(Second);
                    break;
                case WhichType.third:
                    thirdTransform(Third);
                    break;
                default:
                    throw new ArgumentException(); // really should be impossible
            }
        }

        public static implicit operator OneOf<T1, T2, T3>(T1 first)
        {
            return new OneOf<T1, T2, T3>(first);
        }

        public static implicit operator OneOf<T1, T2, T3>(T2 second)
        {
            return new OneOf<T1, T2, T3>(second);
        }

        public static implicit operator OneOf<T1, T2, T3>(T3 third)
        {
            return new OneOf<T1, T2, T3>(third);
        }

        public override bool Equals(object obj)
        {
            var of = obj as OneOf<T1, T2, T3>;
            return of != null &&
                   EqualityComparer<T1>.Default.Equals(First, of.First) &&
                   EqualityComparer<T2>.Default.Equals(Second, of.Second) &&
                   EqualityComparer<T3>.Default.Equals(Third, of.Third) &&
                   FilledType == of.FilledType;
        }

        public override int GetHashCode()
        {
            var hashCode = -1165374187;
            hashCode = hashCode * -1521134295 + EqualityComparer<T1>.Default.GetHashCode(First);
            hashCode = hashCode * -1521134295 + EqualityComparer<T2>.Default.GetHashCode(Second);
            hashCode = hashCode * -1521134295 + EqualityComparer<T3>.Default.GetHashCode(Third);
            hashCode = hashCode * -1521134295 + FilledType.GetHashCode();
            return hashCode;
        }
    }
}
