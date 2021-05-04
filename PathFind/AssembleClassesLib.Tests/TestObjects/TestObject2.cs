using AssembleClassesLib.Attributes;

namespace AssembleClassesLib.Tests.TestObjects
{
    [ClassName(Constants.TestObject2Name)]
    internal sealed class TestObject2
    {
        private readonly int number;

        public int Number => number;

        public TestObject2(int number)
        {
            this.number = number;
        }
    }
}
