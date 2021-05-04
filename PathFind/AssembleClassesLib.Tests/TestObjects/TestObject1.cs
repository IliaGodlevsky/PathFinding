using AssembleClassesLib.Attributes;

namespace AssembleClassesLib.Tests.TestObjects
{
    [ClassName(Constants.TestObject1Name)]
    internal sealed class TestObject1
    {
        public TestObject1(int number)
        {
            this.number = number;
        }

        public int Number => number;

        private readonly int number;
    }
}
