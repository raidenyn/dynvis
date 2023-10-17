using System.Windows.Forms;
using DynVis.Core;
using DynVis.Data.Geometry;


namespace DynVis.Data.GridSurface
{
    public class GridData : ReactionData
    {
        private GridSurface ImportSurface;
        private GeometryGrid GeometryGrid;
        
        public override ISurface3D Surface
        {
            get { return ImportSurface; }
        }

        public override IReactionSystemGeometry Geometry
        {
            get { return GeometryGrid; }
        }

        protected override bool GetNewSurface(IWin32Window ownerForms)
        {
            var F = new FormGridSurface();
            if (F.ShowDialog(ownerForms) == DialogResult.OK)
            {
                ImportSurface = F.GetSurface();
                GeometryGrid = null;
            }
            return ImportSurface != null;
        }

        protected override bool GetNewGeometry(IWin32Window ownerForms)
        {
            if (AllowCreateGeometry)
            {
                var F = new FormGridGeometry(ImportSurface.GetQ1ArrayClone(), ImportSurface.GetQ2ArrayClone());
                if (F.ShowDialog(ownerForms) == DialogResult.OK)
                {
                    GeometryGrid = F.GetGeometry();
                    return true;
                }
            }
            return false;
        }

        protected override bool AllowNewSurface
        {
            get { return true; }
        }

        protected override bool AllowNewGeometry
        {
            get { return ImportSurface != null; }
        }

        protected override bool AllowCalcNewPath
        {
            get { return ImportSurface != null; }
        }
    }
}
