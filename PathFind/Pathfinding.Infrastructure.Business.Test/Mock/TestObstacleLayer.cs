using Pathfinding.Domain.Interface;

namespace Pathfinding.Infrastructure.Business.Test.Mock
{
    internal sealed class TestObstacleLayer : TestLayer<bool>
    {
        private const bool O = false;
        private const bool I = true;

        protected override bool[,] Matrix { get; } = new bool[30, 35]
        {   //0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34
       /*0*/{ O, I, O, O, I, O, O, O, I, O, O, O, I, O, O, O, O, I, O, O, O, I, O, O, O, O, I, O, O, O, I, O, O, O, O },
       /*1*/{ O, O, O, I, O, I, O, O, O, I, O, O, I, O, O, I, O, O, I, O, O, O, O, I, O, O, I, O, O, I, O, O, O, I, O },
       /*2*/{ O, O, I, O, O, O, O, I, O, O, O, I, O, O, O, O, O, I, O, O, I, O, I, O, O, O, O, O, I, O, O, O, I, O, O },
       /*3*/{ O, I, O, O, I, O, O, O, O, O, I, O, I, O, O, I, O, O, O, O, O, O, O, O, I, O, I, O, O, I, O, O, I, O, O },
       /*4*/{ O, O, O, I, O, O, O, I, O, O, O, O, O, I, O, O, O, O, O, O, O, O, I, O, O, O, I, O, O, O, O, I, O, O, I },
       /*5*/{ O, I, O, O, O, O, O, I, O, O, O, I, O, O, O, O, O, I, O, O, I, O, I, O, O, I, O, O, O, O, O, O, I, O, O },
       /*6*/{ O, O, O, O, O, O, O, O, O, I, O, O, O, I, O, O, O, I, O, O, O, O, O, I, O, O, O, O, I, O, O, O, O, O, I },
       /*7*/{ O, O, I, O, O, I, O, O, O, O, O, O, O, O, O, I, O, O, O, O, O, I, O, O, O, O, O, I, O, O, O, O, O, O, O },
       /*8*/{ O, O, O, O, I, O, O, O, O, I, O, O, O, O, O, I, O, O, O, O, O, I, O, I, O, O, O, O, O, O, O, O, I, O, O },
       /*9*/{ O, O, O, I, O, O, I, O, O, O, I, O, O, O, I, O, O, I, O, O, O, O, O, O, O, O, O, O, I, O, O, I, O, O, O },
      /*10*/{ O, O, O, O, O, O, I, O, O, O, O, I, O, O, O, O, O, O, I, O, O, O, O, I, O, O, I, O, O, O, O, O, O, O, I },
      /*11*/{ O, I, O, O, O, O, O, O, O, I, O, O, O, I, O, O, I, O, O, O, I, O, O, O, O, O, O, I, O, O, O, O, O, O, O },
      /*12*/{ O, O, O, O, I, O, O, O, O, O, I, O, O, O, I, O, O, O, O, O, O, O, O, O, O, O, I, O, O, I, O, O, I, O, O },
      /*13*/{ O, I, O, O, O, O, O, O, O, I, O, O, I, O, O, O, O, O, O, O, I, O, O, I, O, I, O, O, O, O, O, I, O, O, O },
      /*14*/{ O, O, O, O, O, I, O, O, I, O, O, O, O, I, O, O, O, O, O, I, O, I, O, O, O, O, O, I, O, O, O, O, O, O, I },
      /*15*/{ O, O, O, I, O, O, I, O, O, O, I, O, O, O, O, I, O, O, O, O, O, O, O, I, O, I, O, O, I, O, O, O, O, O, O },
      /*16*/{ O, O, O, O, I, O, O, O, I, O, O, O, O, O, I, O, O, O, O, O, I, O, O, I, O, O, O, I, O, O, O, O, I, O, O },
      /*17*/{ O, O, O, O, O, I, O, I, O, O, O, O, O, O, I, O, O, O, I, O, O, O, O, O, O, O, I, O, I, O, O, O, O, O, O },
      /*18*/{ O, O, I, O, O, O, I, O, O, O, O, I, O, O, O, O, I, O, O, O, O, O, O, I, O, O, O, O, O, O, O, O, I, O, O },
      /*19*/{ O, O, O, O, I, O, O, O, O, I, O, O, O, I, O, O, O, O, O, O, O, I, O, O, O, O, I, O, O, O, O, O, O, O, I },
      /*20*/{ O, O, I, O, O, O, O, O, O, O, O, I, O, O, O, O, I, O, O, O, I, O, O, O, O, O, I, O, O, O, O, O, I, O, O },
      /*21*/{ O, O, O, I, O, O, O, O, O, I, O, O, O, O, O, I, O, O, O, O, I, O, O, I, O, O, O, I, O, O, O, O, I, O, O },
      /*22*/{ O, O, O, O, O, O, I, O, O, O, O, O, O, I, O, O, O, O, I, O, O, O, O, O, O, I, O, O, I, O, O, O, I, O, O },
      /*23*/{ O, O, O, I, O, O, O, I, O, O, O, O, O, O, O, O, O, I, O, O, I, O, O, O, O, O, O, I, O, O, I, O, O, O, O },
      /*24*/{ O, I, O, O, O, O, I, O, O, O, I, O, O, I, O, O, O, O, I, O, O, O, O, O, O, O, O, O, O, O, I, O, O, I, O },
      /*25*/{ O, O, O, I, O, O, O, O, O, I, O, O, O, I, O, O, O, O, O, O, O, O, O, O, I, O, O, O, O, O, I, O, O, O, O },
      /*26*/{ O, O, I, O, O, O, O, O, O, O, O, O, I, O, O, O, I, O, O, O, O, I, O, O, O, O, O, I, O, I, O, O, O, O, O },
      /*27*/{ O, I, O, O, O, O, O, O, I, O, O, O, O, O, O, I, O, O, O, O, O, I, O, I, O, O, O, O, O, O, O, I, O, O, O },
      /*28*/{ O, O, O, I, O, O, I, O, O, O, O, I, O, O, O, O, O, O, I, O, O, O, O, O, I, O, O, I, O, O, O, O, O, O, O },
      /*29*/{ O, O, I, O, O, O, O, O, O, O, O, O, O, O, O, I, O, I, O, O, I, O, O, O, O, I, O, O, O, O, O, I, O, O, O }
        };

        protected override void Assign(IVertex vertex, bool value)
        {
            vertex.IsObstacle = value;
        }
    }
}
