using DynVis.Core;
using DynVis.Core.PropertyEditor;
using DynVis.Draw.Surface.Properties;

namespace DynVis.Draw.Surface
{
    class SurfacePropertyProvider: IPropertiesObject
    {
        public SurfacePropertyProvider(DrawSurfaceEngineGDI_2D surfaceEngine)
        {
            SurfaceEngine = surfaceEngine;
        }

        public SurfacePropertyProvider(DrawSurfaceEngineGDI_2D surfaceEngine, Proc updateViewProc)
            : this(surfaceEngine)
        {
            UpdateViewProc = updateViewProc;
        }

        public readonly DrawSurfaceEngineGDI_2D SurfaceEngine;

        public readonly Proc UpdateViewProc;

        public object EditableObject
        {
            get { return SurfaceEngine; }
        }

        public string PropertiesObjectName
        {
            get { return LangResource.Surface2DCustomization; }
        }

        public string SectionName
        {
            get { return LangResource.SurfaceSection; }
        }

       

        public IPropertyEditorControl GetPropertiesEditor()
        {
            return new SurfacePropertyEditor2D { EditingObject = SurfaceEngine, UpdateViewObject = UpdateViewProc };
        }
    }
}
