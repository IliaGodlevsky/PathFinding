using ConsoleVersion.Interface;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.Views
{
    internal sealed class EndPointsView : View, IView
    {
        public EndPointsView(EndPointsViewModel model) : base(model)
        {
            model.EndPointsMessages = new[] { MessagesTexts.SourceVertexChoiceMsg, MessagesTexts.TargetVertexChoiceMsg };
            model.ReplaceIntermediatesMessages = new[] { MessagesTexts.IntermediateToReplaceMsg, MessagesTexts.PlaceToPutIntermediateMsg };
        }
    }
}
