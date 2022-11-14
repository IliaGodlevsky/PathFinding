using Pathfinding.App.Console.Menu.Realizations.Delegates;

namespace Pathfinding.App.Console.Menu.Realizations
{
    internal sealed class ConditionPair
    {
        private Condition Condition { get; }

        public string FailMessage { get; }

        public ConditionPair(Condition condition, string failMessage)
        {
            Condition = condition;
            FailMessage = failMessage;
        }

        public bool IsValidCondition()
        {
            return Condition == null || Condition();
        }
    }
}
