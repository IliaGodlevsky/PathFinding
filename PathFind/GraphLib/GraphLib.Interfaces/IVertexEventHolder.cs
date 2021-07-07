using System;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// Provides functionality to manipulate
    /// with <see cref="IVertex"/> interface
    /// </summary>
    public interface IVertexEventHolder
    {
        /// <summary>
        /// A method, that contains a logic
        /// for changing vertex obstacle status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Reverse(object sender, EventArgs e);

        /// <summary>
        /// A method, that contains logic for 
        /// changing <see cref="IVertex"/> cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChangeVertexCost(object sender, EventArgs e);

        void SubscribeVertices(IGraph graph);

        void UnsubscribeVertices(IGraph graph);
    }
}
