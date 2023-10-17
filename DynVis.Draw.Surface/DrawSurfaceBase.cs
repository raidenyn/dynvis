using System.ComponentModel;
using System.Windows.Forms;
using DynVis.Core;

namespace DynVis.Draw.Surface
{
    internal class DrawSurfaceBase: BaseControl
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ISurface3D Surface
        { get; set;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IPath Path
        {
            get; set;
        }
    }
}
