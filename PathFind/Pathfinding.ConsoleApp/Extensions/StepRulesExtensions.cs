using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class StepRulesExtensions
    {
        public static string ToStringRepresentation(this StepRules stepRules)
        {
            return stepRules switch
            {
                StepRules.Default => "Default",
                StepRules.Landscape => "Landscape",
                _ => "",
            };
        }
    }
}
