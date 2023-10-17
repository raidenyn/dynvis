using System;
using DynVis.Core.PropertyEditor;

namespace DynVis.Draw.Surface
{
    public partial class SurfacePropertyEditor3D : ObjectPropertiesEditorBase
    {
        public SurfacePropertyEditor3D()
        {
            InitializeComponent();
        }

        private void SurfacePropertyEditor3D_EditingObjectChanged(object sender, EventArgs e)
        {
            drawSurfaceEngineGL3DBindingSource.Clear();
            drawSurfaceEngineGL3DBindingSource.Add(EditingObject);

        }

        private void drawSurfaceEngineGL3DBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            RiseEditingObjectValueChanged();
        }
    }
}
