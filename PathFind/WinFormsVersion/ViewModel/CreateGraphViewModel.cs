using GraphLibrary.Vertex.Interface;
using GraphLibrary.ViewModel;
using GraphLibrary.ViewModel.Interface;
using System;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.ViewModel
{
    internal class CreateGraphViewModel : AbstractCreateGraphModel
    {
        public CreateGraphViewModel(IMainModel model) : base(model)
        {            

        }

        public override void CreateGraph(Func<IVertex> generator)
        {
            base.CreateGraph(generator);
            (model as MainWindowViewModel).Window?.Close();
        }

        public void CreateGraph(object sender, EventArgs e) => CreateGraph(() => new WinFormsVertex());

        public void CancelCreateGraph(object sender, EventArgs e) => (model as MainWindowViewModel)?.Window.Close();
        
    }
}
