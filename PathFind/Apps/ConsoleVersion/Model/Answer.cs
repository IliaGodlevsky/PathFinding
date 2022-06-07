using System;
using ValueRange;

namespace ConsoleVersion.Model
{
    internal readonly struct Answer : IComparable
    {
        private static readonly Answer Yes = new Answer(1, "Yes");

        private static readonly Answer No = new Answer(0, "No");

        private static readonly InclusiveValueRange<Answer> Range = new InclusiveValueRange<Answer>(Yes, No);

        private readonly int value;
        private readonly string name;

        private Answer(int value, string name)
        {
            this.value = value;
            this.name = name;
        }

        public int CompareTo(object obj)
        {
            if (obj is Answer answer)
            {
                return answer.value.CompareTo(value);
            }
            throw new ArgumentException("object must be answer");
        }

        public override int GetHashCode()
        {
            return value.GetHashCode() ^ name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case int value:
                    return value.Equals(this.value);
                case string name:
                    return name.Equals(this.name, StringComparison.InvariantCultureIgnoreCase);
                default:
                    return false;
            }
        }

        public override string ToString() => name;

        public static bool operator == (Answer value1, Answer value2)
        {
            return value1.value == value2.value && value1.name == value2.name;
        }

        public static bool operator !=(Answer value1, Answer value2)
        {
            return !(value1 == value2);
        }

        public static bool operator true(Answer value)
        {
            return value.value == 1;
        }

        public static bool operator false(Answer value)
        {
            return value.value == 0;
        }

        public static explicit operator int(Answer value)
        {
            return value.value;
        }

        public static explicit operator string(Answer answer)
        {
            return answer.name;
        }
    }
}
