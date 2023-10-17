using System.Drawing;
using System.Windows.Forms;
using DynVis.Core.PropertyEditor;
using DynVis.Draw.OpenGL;
using DynVis.Math;
using Tao.OpenGl;

namespace DynVis.Draw
{
    public abstract class BaseEngine : OpenGLEngine
    {
        protected BaseEngine(Control ownerControl) : base(ownerControl)
        {
            SetProperties();
        }

        public Point3D CenterPoint = new Point3D(0, 0, -5);
        public Point3D Light1PositionPoint = new Point3D(10, 10, 3);
        public Point3D Light2PositionPoint = new Point3D(-10, -10, 1);
        public IPoint3D RotateCenterPoint = new Point3D(0, 0, 0);

        [SavableProperty]
        [TransactingEditable]
        public Color BackgroundColor{get; set;}

        [SavableProperty]
        [TransactingEditable]
        public PolygonMode PolygonMode { get; set; }

        public bool SetCenterPointToRotateCenterPoint = true;

        public double Scale = 1.0;

        private readonly float[] RotateMatrix = CreateIndentyMatrixf(4);

        private void SetProperties()
        {
            BackgroundColor = Color.FromArgb(128, 128, 192);

            PolygonMode = PolygonMode.Fill;
        }

        protected void RotateAlongCenter()
        {
            TranslateP3D(RotateCenterPoint, TranslateType.Froward);
            Gl.glMultMatrixf(RotateMatrix);
            TranslateP3D(RotateCenterPoint, TranslateType.Revers);
        }

        public void RotateAlongScreenAxesX(double x)
        {
            SetRotateMatrix(RotateMatrix, x, Axes.X);
        }

        public void RotateAlongScreenAxesY(double y)
        {
            SetRotateMatrix(RotateMatrix, y, Axes.Y);
        }

        protected void TranslateToCenter(TranslateType tt)
        {
            TranslateP3D(CenterPoint, tt);
            if (SetCenterPointToRotateCenterPoint) TranslateP3D(RotateCenterPoint, TranslateType.Revers);
        }

        protected void TranslateToCenter()
        {
            TranslateToCenter(TranslateType.Froward);
        }

        protected void SetCurrentPoligoneMode()
        {
            switch (PolygonMode)
            {
                case PolygonMode.Fill: Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
                    return;
                case PolygonMode.Line: Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
                    return;
                case PolygonMode.Point: Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_POINT);
                    return;
            }
        }
    }

    public enum PolygonMode { Fill, Line, Point }
}
