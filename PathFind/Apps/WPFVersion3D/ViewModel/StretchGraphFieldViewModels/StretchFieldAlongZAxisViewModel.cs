using WPFVersion3D.Axes;
using WPFVersion3D.Enums;

namespace WPFVersion3D.ViewModel.StretchGraphFieldViewModels
{
    internal sealed class StretchFieldAlongZAxisViewModel : StretchFieldAlongAxisViewModel
    {
        public override string Title { get; }

        protected override double[] AdditionalOffset { get; }

        public StretchFieldAlongZAxisViewModel() 
            : base(new Applicate(), MessageTokens.StretchAlongXAxisModel)
        {
            AdditionalOffset = new double[] { 1, 0, 0 };
            Title = "Z axis";
        }        
    }
}
