using System;
using DynVis.Core.PropertyEditor;

namespace DynVis.Draw.Geometry
{
    public partial class GeometryDrawParamsEditor : ObjectPropertiesEditorBase
    {
        public GeometryDrawParamsEditor()
        {
            InitializeComponent();
        }

        private void GeometryDrawParamsEditor_EditingObjectChanged(object sender, EventArgs e)
        {
            geometryDrawEngineBindingSource.Add(EditingObject);
        }

        private void GeometryDrawParamsEditor_AcceptChanges(object sender, EventArgs e)
        {
            geometryDrawEngineBindingSource.EndEdit();
        }

        private void GeometryDrawParamsEditor_RollbackChanges(object sender, EventArgs e)
        {
            geometryDrawEngineBindingSource.CancelEdit();
        }

        private void geometryDrawEngineBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            RiseEditingObjectValueChanged();
        }
        
    }
}
