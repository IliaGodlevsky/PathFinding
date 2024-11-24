namespace Pathfinding.ConsoleApp.Model
{
    internal record class StartAlgorithmModel(string AlgorithmName, 
        string StepRule, 
        string Heuristics, 
        double? Weight);
}
