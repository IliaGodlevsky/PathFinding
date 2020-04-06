using ConsoleVersion.InputClass;
using GraphLibrary.RoleChanger;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using System;
using System.Linq;

namespace SearchAlgorythms.RoleChanger
{
    public class ConsoleVertexRoleChanger : AbstractVertexRoleChanger
    {

        public ConsoleVertexRoleChanger(AbstractGraph graph) : base(graph)
        {

        }

        public override void ChangeTopText(object sender, EventArgs e)
        {
            (sender as ConsoleVertex).Text = 
                Input.InputNumber("Enter new top value: ", 9, 1).ToString();
        }

        public override void ReversePolarity(object sender, EventArgs e)
        {
            IVertex top = sender as ConsoleVertex;
            Reverse(ref top);
        }

        public override void SetDestinationPoint(object sender, EventArgs e)
        {
            ConsoleVertex top = sender as ConsoleVertex;
            if (!IsRightDestination(top))
                return;
            top.IsEnd = true;
            top.MarkAsEnd();
            graph.End = top;
        }

        public override void SetStartPoint(object sender, EventArgs e)
        {
            ConsoleVertex top = sender as ConsoleVertex;
            if (!IsRightDestination(top))
                return;
            top.IsStart = true;
            top.MarkAsStart();
            graph.Start = top;
        }
    }
}
