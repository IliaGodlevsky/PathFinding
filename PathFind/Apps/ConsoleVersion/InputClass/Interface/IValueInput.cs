using Common.ValueRanges;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.InputClass.Interface
{
    internal interface IValueInput
    {
        int InputNumber(string accompanyingMessage, int upperRangeValue = int.MaxValue, int lowerRangeValue = 0);

        int InputNumber(string accompanyingMessage, InclusiveValueRange<int> rangeOfValidInput);

        Coordinate2D InputPoint(int upperPossibleXValue, int upperPossibleYValue);

        InclusiveValueRange<int> InputRange(InclusiveValueRange<int> rangeOfValiInput);

        IVertex InputVertex(Graph2D graph2D);
    }
}
