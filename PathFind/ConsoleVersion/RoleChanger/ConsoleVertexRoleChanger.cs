using ConsoleVersion.InputClass;
using GraphLibrary.RoleChanger;
using ConsoleVersion.Vertex;
using System;
using GraphLibrary.Graph;
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
    }
}
