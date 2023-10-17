using System;
using DynVis.Core.PropertyEditor;

namespace DynVis.Draw.Surface
{
    public partial class SurfacePropertyEditor2D : ObjectPropertiesEditorBase
    {
        public SurfacePropertyEditor2D()
        {
            InitializeComponent();
        }

        private void SurfacePropertyEditor_EditingObjectChanged(object sender, EventArgs e)
        {
            drawSurfaceEngineGDI2DBindingSource.Clear();
            drawSurfaceEngineGDI2DBindingSource.Add(EditingObject);
        }

        private void drawSurfaceEngineGDI2DBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            RiseEditingObjectValueChanged();
        }

    }
}
