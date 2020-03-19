using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public class RandomValuedCreate : RandomCreate
    {
        public RandomValuedCreate(int percentOfObstacles, int width, int height, int buttonWidth,
            int buttonHeight, int placeBetweenButtons) : base(percentOfObstacles,width, height, buttonWidth,
            buttonHeight, placeBetweenButtons)
        {

        }

        public override void CreateGraphTop(ref Button button)
        {
            base.CreateGraphTop(ref button);
            (button as GraphTop).Text = (rand.Next(9) + 1).ToString();
        }
    }
}
