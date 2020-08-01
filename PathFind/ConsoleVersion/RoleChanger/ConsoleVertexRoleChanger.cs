using ConsoleVersion.InputClass;
using GraphLibrary.RoleChanger;
using ConsoleVersion.Vertex;
using System;
using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using GraphLibrary.Constants;

namespace ConsoleVersion.RoleChanger
{
    public class ConsoleVertexRoleChanger : AbstractVertexRoleChanger
    {

        public ConsoleVertexRoleChanger(AbstractGraph graph) : base(graph)
        {

        }

        public override void ChangeTopText(object sender, EventArgs e) => (sender as ConsoleVertex).Text =
                Input.InputNumber(Res.NewTopValueMsg, 
                                  Const.MAX_VERTEX_VALUE, 
                                  Const.MIN_VERTEX_VALUE).ToString();

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
