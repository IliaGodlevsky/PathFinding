using NUnit.Framework;
using System;

namespace Pathfinding.TestUtils.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public sealed class IntegrationTestAttribute : CategoryAttribute
    {
        public IntegrationTestAttribute() 
            : base("Integration Tests") 
        { 

        }
    }
}
