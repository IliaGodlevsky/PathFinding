using WPFVersion3D.Axes;
using WPFVersion3D.Enums;

namespace WPFVersion3D.ViewModel.StretchGraphFieldViewModels
{
    internal sealed class StretchFieldAlongYAxisViewModel : StretchFieldAlongAxisViewModel
    {
        public override string Title { get; }

        protected override double[] AdditionalOffset { get; }

        public StretchFieldAlongYAxisViewModel() 
            : base(new Ordinate(), MessageTokens.StretchAlongYAxisModel)
        {            
            AdditionalOffset = new double[] { 0, 1, 0 };
            Title = "Y axis";
        }       
    }
}