using System.Windows.Forms;
using DynVis.Core;
using DynVis.Data.CalculationMethods.LEPS;


namespace DynVis.Data.CalculationMethods
{
    public class CalculationLEPS : ReactionData
    {
        private SurfaceLEPS SurfaceLEPS;
        private GeometryLEPS GeometryLEPS;

        public override ISurface3D Surface { get { return SurfaceLEPS; } }
        public override IReactionSystemGeometry Geometry { get { return GeometryLEPS; } }

        protected override bool GetNewSurface(IWin32Window ownerForms)
        {
            var F = new FormLEPSEditor();
            if (F.ShowDialog(ownerForms) == DialogResult.OK)
            {
                SurfaceLEPS = F.GetSurface();
                GeometryLEPS = new GeometryLEPS(SurfaceLEPS);

                RiseGeometryChanged();
                return true;
            }
            return false;
        }

        protected override bool GetNewGeometry(IWin32Window ownerForms)
        {
            return false;
        }

        protected override bool AllowNewSurface
        {
            get { return true; }
        }

        protected override bool AllowNewGeometry
        {
            get { return false; }
        }

        protected override bool AllowCalcNewPath
        {
            get { return true; }
        }
    }
}
