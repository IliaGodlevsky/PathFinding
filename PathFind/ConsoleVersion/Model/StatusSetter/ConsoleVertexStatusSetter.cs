using ConsoleVersion.InputClass;
using GraphLibrary.StatusSetter;
using ConsoleVersion.Vertex;
using System;
using GraphLibrary.Graph;
using GraphLibrary.Constants;

namespace ConsoleVersion.StatusSetter
{
    public class ConsoleVertexStatusSetter : AbstractVertexStatusSetter
    {

        public ConsoleVertexStatusSetter(AbstractGraph graph) : base(graph)
        {

        }

        public override void ChangeVertexValue(object sender, EventArgs e) =>
            (sender as ConsoleVertex).Text = Input.InputNumber(Res.NewTopValueMsg,
                Const.MAX_VERTEX_VALUE, Const.MIN_VERTEX_VALUE).ToString();

        protected override int GetWheelDelta(EventArgs e)
        {
            return default;
        }
    }
}
