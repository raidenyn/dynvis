using System.Windows.Forms;
using DynVis.Core;

namespace DynVis.Data.CalculationMethods.LEPS.Controls
{
    internal partial class AtomsInteractionParamsLEPS : BaseControl
    {
        public AtomsInteractionParamsLEPS()
        {
            InitializeComponent();
        }

        public string DoubleFormat = "F4";

        public AtomAtomProperty GetAtomAtomProperty()
        {
            var result = new AtomAtomProperty
                             {
                                 k = KEditor.Value,
                                 B = BEditor.GetValue(DimReverseLenth.ReverseAngstrom),
                                 De = DeEditor.GetValue(DimEnergy.Joule, ScaleCoeff.kilo),
                                 r0 = R0Editor.GetValue(DimLength.Angstrom)
                             };

            return result;
        }

        public void SetAtomAtomProperty(AtomAtomProperty aap)
        {
            KEditor.SetValue(aap.k, DimNone.None);
            BEditor.SetValue(aap.B, DimReverseLenth.ReverseAngstrom);
            DeEditor.SetValue(aap.De, DimEnergy.Joule, ScaleCoeff.kilo);
            R0Editor.SetValue(aap.r0, DimLength.Angstrom);
        }

        public string Title
        {
            get
            {
                return groupBox.Text;
            }
            set
            {
                groupBox.Text = value;
            }
        }
    }
}
