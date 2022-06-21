using System;
using System.Linq;

namespace ConsoleVersion.Model
{
    internal readonly struct Answer : IComparable, IComparable<Answer>, IEquatable<Answer>
    {
        private const StringComparison IgnoreCase = StringComparison.OrdinalIgnoreCase;

        private static readonly Answer Default = new Answer(-1, string.Empty);
        public static readonly Answer Yes = new Answer(1, nameof(Yes));
        public static readonly Answer No = new Answer(0, nameof(No));

        private static readonly Answer[] Answers = new[] { Yes, No };

        private readonly int value;
        private readonly string display;

        private Answer(int value, string display)
        {
            this.display = display;
            this.value = value;
        }

        public int CompareTo(Answer other)
        {
            return value.CompareTo(other.value);
        }

        public int CompareTo(object obj)
        {
            if (obj is Answer answer)
            {
                return this.CompareTo(answer);
            }
            throw new ArgumentException("Wrong argument", nameof(obj));
        }

        public bool Equals(Answer other)
        {
            return other.value == value && display.Equals(other.display, IgnoreCase) == true;
        }

        public override bool Equals(object obj)
        {
            if (obj is Answer answer)
            {
                return this.Equals(answer);
            }

            return false;
        }

        public override string ToString()
        {
            return display;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value, display);
        }

        public static bool TryParse(string input, out Answer result)
        {
            result = default;
            if (int.TryParse(input, out int value))
            {
                result = FirstOrDefaultAnswer(Answers, answer => answer.value.Equals(value));
                return !result.Equals(Default);
            }
            result = FirstOrDefaultAnswer(Answers, answer => answer.display.Equals(input, IgnoreCase));
            return !result.Equals(Default);
        }

        public static implicit operator bool(Answer answer)
        {
            return answer.value == 1;
        }

        public static implicit operator int(Answer answer)
        {
            return answer.value;
        }

        private static Answer FirstOrDefaultAnswer(Answer[] answers, Func<Answer, bool> predicate)
        {
            return answers.Any(predicate) ? answers.FirstOrDefault(predicate) : Answer.Default;
        }
    }
}
