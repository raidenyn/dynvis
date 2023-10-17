using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.PropertyEditor;
using DynVis.Draw.Surface.Properties;

namespace DynVis.Draw.Surface
{
    class DrawSurface3DProvider : DrawProvider
    {
        public DrawSurface3DProvider(Control ownerControl)
        {
            DrawSurfaceEngine = new DrawSurfaceEngineGL_3D(ownerControl);
            SurfaceProperty = new SurfacePropertyProvider(DrawSurfaceEngine, FullRedraw);
            GlobalProperiesEditor.RegisterEditableObject(SurfaceProperty);
        }

        private readonly DrawSurfaceEngineGL_3D DrawSurfaceEngine;

        protected override BaseEngine BaseEngine
        {
            get { return DrawSurfaceEngine; }
        }

        private ISurface3D _Surface;

        public ISurface3D Surface
        {
            get { return _Surface; }
            set
            {
                _Surface = value;
                DrawSurfaceEngine.ClearCashe();
            }
        }

        public IPath Path
        {
            get;
            set;
        }

        public PolygonMode SurfaceMode
        {
            get { return DrawSurfaceEngine.PolygonMode; }
            set { DrawSurfaceEngine.PolygonMode = value; }
        }

        public void Draw()
        {
            if (DrawSurfaceEngine.BeginScene())
            {
                DrawSurfaceEngine.DrawSurface(Surface);

                DrawSurfaceEngine.DrawCurrentPoint(Surface);

                DrawSurfaceEngine.DrawPath(Surface, Path);

                DrawSurfaceEngine.EndScene();
            }
        }

        public void FullRedraw()
        {
            DrawSurfaceEngine.ClearCashe();
            Draw();
        }

        private readonly SurfacePropertyProvider SurfaceProperty;

        class SurfacePropertyProvider : IPropertiesObject
        {
            public SurfacePropertyProvider(DrawSurfaceEngineGL_3D surfaceEngine)
            {
                SurfaceEngine = surfaceEngine;
            }

            public SurfacePropertyProvider(DrawSurfaceEngineGL_3D surfaceEngine, Proc updateViewProc)
                : this(surfaceEngine)
            {
                UpdateViewProc = updateViewProc;
            }

            public readonly DrawSurfaceEngineGL_3D SurfaceEngine;

            public readonly Proc UpdateViewProc;

            public object EditableObject
            {
                get { return SurfaceEngine; }
            }

            public string PropertiesObjectName
            {
                get { return LangResource.Surface3DCustomization; }
            }

            public string SectionName
            {
                get { return LangResource.SurfaceSection; }
            }



            public IPropertyEditorControl GetPropertiesEditor()
            {
                return new SurfacePropertyEditor3D { EditingObject = SurfaceEngine, UpdateViewObject = UpdateViewProc };
            }
        }
    }
}
