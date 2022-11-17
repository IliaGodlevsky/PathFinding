using Pathfinding.App.Console.Model.Menu.Delegates;

namespace Pathfinding.App.Console.Model.Menu
{
    internal sealed class ConditionPair
    {
        private readonly Condition condition;

        public string FailMessage { get; }

        public ConditionPair(Condition condition, string failMessage)
        {
            this.condition = condition;
            FailMessage = failMessage;
        }

        public bool IsValidCondition()
        {
            return condition == null || condition();
        }
    }
}
