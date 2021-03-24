using GraphLib.Base;
using GraphLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Realizations.Tests.TestObjects
{
    internal sealed class TestCoordinate : BaseCoordinate
    {
        public TestCoordinate(params int[] coordinates) :
            base(1, coordinates)
        {

        }
    }
}
