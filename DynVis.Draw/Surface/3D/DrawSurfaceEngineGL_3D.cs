using System.Drawing;
using DynVis.Core.PropertyEditor;
using M=System.Math;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Math;
using Tao.OpenGl;

namespace DynVis.Draw.Surface
{
    public class DrawSurfaceEngineGL_3D : BaseEngine
    {
        public DrawSurfaceEngineGL_3D(Control ownerControl) : base(ownerControl)
        {
            SetProperties();
        }


        public const string ParamName_Blend = "SurfaceBlend";
        public const string ParamName_CountQ1 = "Q1SurfaceCount";
        public const string ParamName_CountQ2 = "Q2SurfaceCount";
        public const string ParamName_ScaleE = "DefaultSurfaceScaleE";
        public const string ParamName_CurrentPointSize = "SurfaceCurrentPointSize";
        public const string ParamName_SurfaceLineWidth = "SurfaceLineWidth";
        public const string ParamName_PathLineWidth = "PathLineWidth";

        [TransactingEditable]
        public float Blend
        {
            get
            {
                return GlobalParams.GetFloat(ParamName_Blend, 1.0f);
            }
            set
            {
                GlobalParams.SetValue(ParamName_Blend, value);
            }
        }

        [TransactingEditable]
        [SavableProperty]
        public int CountQ1
        {
            get { return GlobalParams.GetInt(ParamName_CountQ1, 100); }
            set { GlobalParams.SetValue(ParamName_CountQ1, value); }
        }
        [TransactingEditable]
        [SavableProperty]
        public int CountQ2
        {
            get { return GlobalParams.GetInt(ParamName_CountQ2, 100); }
            set { GlobalParams.SetValue(ParamName_CountQ2, value); }
        }
        [TransactingEditable]
        [SavableProperty]
        public double ScaleE
        {
            get { return GlobalParams.GetDouble(ParamName_ScaleE, 1.0); }
            set { GlobalParams.SetValue(ParamName_ScaleE, value); }
        }
        public double ScaleQ1 = 3.0f;
        public double ScaleQ2 = 3.0f;

        [TransactingEditable]
        [SavableProperty]
        public float SurfaceLineWidth
        {
            get
            {
                return GlobalParams.GetFloat(ParamName_SurfaceLineWidth, 1f);
            }
            set
            {
                GlobalParams.SetValue(ParamName_SurfaceLineWidth, value);
            }
        }

        [TransactingEditable]
        [SavableProperty]
        public float PathLineWidth
        { get;
            set;
        }

        [TransactingEditable]
        [SavableProperty]
        public Color PathLineColor
        { get; set;
        }

        [TransactingEditable]
        [SavableProperty]
        public double CurrentPointSize
        {
            get; set;
        }

        [TransactingEditable]
        [SavableProperty]
        public Color CurrentPointColor
        { get; set;
        }

        public int CurrentPointSlice = 10;
        public int CurrentPointStack = 10;


        private void SetProperties()
        {
            BackgroundColor = Color.FromArgb(255, 255, 255);
            PolygonMode = PolygonMode.Line;

            PathLineColor = Color.Black;
            PathLineWidth = 2f;
            CurrentPointSize = 0.025;
            CurrentPointColor = Color.Black;
        }

        private int SurfaceList;
        private bool RecalcSurface = true;

        private static double GetNormalCoeffE(ISurface3D surface)
        {
            return 1 / (surface.MaxE.Value - surface.MinE.Value);
        }

        private static double GetNormalCoeffQ1(ISurface3D surface, double Q1Normal)
        {
            return Q1Normal / (surface.Axes1.MaxValue - surface.Axes1.MinValue);
        }

        private static double GetNormalCoeffQ2(ISurface3D surface, double Q2Normal)
        {
            return Q2Normal / (surface.Axes2.MaxValue - surface.Axes2.MinValue);
        }

        private static double GetQ1Normal(ISurface3D surface)
        {
            return 1.0;
        }

        private static double GetQ2Normal(ISurface3D surface)
        {
            var MaxMinQ1Delate = surface.Axes1.MaxValue - surface.Axes1.MinValue;
            var MaxMinQ2Delate = surface.Axes2.MaxValue - surface.Axes2.MinValue;

            return GetQ1Normal(surface) * MaxMinQ2Delate / MaxMinQ1Delate;
        }

        private static Point3D[,] GetNormalizeSurfaceArray(ISurface3D surface, int countQ1, int countQ2, double XNormal, double YNormal)
        {
            var StepQ1 = (surface.Axes1.MaxValue - surface.Axes1.MinValue) / (countQ1 + 1);
            var StepQ2 = (surface.Axes2.MaxValue - surface.Axes2.MinValue) / (countQ2 + 1);

            var result = new Point3D[countQ1+3, countQ2+3];

            var normalCoeffE = GetNormalCoeffE(surface);
            var normalCoeffQ1 = GetNormalCoeffQ1(surface, XNormal);
            var normalCoeffQ2 = GetNormalCoeffQ2(surface, YNormal);

            for (var q1 = 0; q1 < countQ1; q1++)
            {
                for (var q2 = 0; q2 < countQ2; q2++)
                {
                    var p = new Point3D
                                {
                                    X = (surface.Axes1.MinValue + q1*StepQ1),
                                    Y = (surface.Axes2.MinValue + q2*StepQ2)
                                };
                    p.Z = surface.E(p.X,p.Y);

                    if (p.Z > surface.MaxE.Value) p.Z = surface.MaxE.Value;

                    p.X *= normalCoeffQ1;
                    p.Y *= normalCoeffQ2;
                    p.Z *= normalCoeffE;

                    result[q1, q2] = p;
                }
            }

            return result;
        }

        private void CreateSurfaceList(ISurface3D surface)
        {
            if (SurfaceList == 0) SurfaceList = Gl.glGenLists(1);

            var MaxMinQ1Delate = surface.Axes1.MaxValue - surface.Axes1.MinValue;
            var MaxMinQ2Delate = surface.Axes2.MaxValue - surface.Axes2.MinValue;

            var Q1Coeff = GetQ1Normal(surface);
            var Q2Coeff = GetQ2Normal(surface);

            var MaxMinEDelate = surface.MaxE.Value - surface.MinE.Value;
            var MaxE = surface.MaxE.Value / MaxMinEDelate;
            var MinE = surface.MinE.Value / MaxMinEDelate;


            var MaxQ1 = Q1Coeff * surface.Axes1.MaxValue / MaxMinQ1Delate;
            var MinQ1 = Q1Coeff * surface.Axes1.MinValue / MaxMinQ1Delate;


            var MaxQ2 = Q2Coeff * surface.Axes2.MaxValue / MaxMinQ2Delate;
            var MinQ2 = Q2Coeff * surface.Axes2.MinValue / MaxMinQ2Delate;

            var SurfaceArray = GetNormalizeSurfaceArray(surface, CountQ1, CountQ2, Q1Coeff, Q2Coeff);


            Gl.glNewList(SurfaceList, Gl.GL_COMPILE);

            Gl.glLineWidth(SurfaceLineWidth);

            Gl.glTranslated(-(MaxQ1 + MinQ1) / 2.0, -(MaxQ2 + MinQ2) / 2.0, -(MaxE + MinE) / 2.0);

            Gl.glBegin(Gl.GL_TRIANGLES);

            for (var u = 1; u < CountQ1-2; u += 1)
            {
                for (var v = 1; v < CountQ2-2; v += 1)
                {
                    var pC = SurfaceArray[u, v];
                    var pT = SurfaceArray[u + 1, v];
                    var pT_T = SurfaceArray[u + 2, v];
                    var pT_L = SurfaceArray[u + 1, v - 1];
                    var pTR = SurfaceArray[u + 1, v + 1];
                    var pTR_T = SurfaceArray[u + 2, v + 1];
                    var pTR_R = SurfaceArray[u + 1, v + 2];
                    var pR = SurfaceArray[u, v + 1];
                    var pR_R = SurfaceArray[u, v + 2];
                    var pR_B = SurfaceArray[u - 1, v + 1];
                    var pB = SurfaceArray[u - 1, v];
                    var pL = SurfaceArray[u, v - 1];

                    var pT_R = pTR;
                    var pT_B = pC;
                    var pR_T = pTR;
                    var pR_L = pC;
                    var pTR_L = pT;
                    var pTR_B = pR;


                    var normalC_TL = PointsFunc.NormalVector3D(pC, pT, pL);
                    var normalC_TR = PointsFunc.NormalVector3D(pC, pR, pT);
                    var normalC_BL = PointsFunc.NormalVector3D(pC, pB, pL);
                    var normalC_BR = PointsFunc.NormalVector3D(pC, pR, pB);

                    var normalT_TL = PointsFunc.NormalVector3D(pT, pT_T, pT_L);
                    var normalT_TR = PointsFunc.NormalVector3D(pT, pT_R, pT_T);
                    var normalT_BL = PointsFunc.NormalVector3D(pT, pT_B, pT_L);
                    var normalT_BR = PointsFunc.NormalVector3D(pT, pT_R, pT_B);

                    var normalR_TL = PointsFunc.NormalVector3D(pR, pR_T, pR_L);
                    var normalR_TR = PointsFunc.NormalVector3D(pR, pR_R, pR_T);
                    var normalR_BL = PointsFunc.NormalVector3D(pR, pR_B, pR_L);
                    var normalR_BR = PointsFunc.NormalVector3D(pR, pR_R, pR_B);

                    var normalTR_TL = PointsFunc.NormalVector3D(pTR, pTR_T, pTR_L);
                    var normalTR_TR = PointsFunc.NormalVector3D(pTR, pTR_R, pTR_T);
                    var normalTR_BL = PointsFunc.NormalVector3D(pTR, pTR_B, pTR_L);
                    var normalTR_BR = PointsFunc.NormalVector3D(pTR, pTR_R, pTR_B);

                    var AvgNormalC = (normalC_TL + normalC_TR + normalC_BL + normalC_BR) * 0.25;
                    var AvgNormalT = (normalT_TL + normalT_TR + normalT_BL + normalT_BR) * 0.25;
                    var AvgNormalR = (normalR_TL + normalR_TR + normalR_BL + normalR_BR) * 0.25;
                    var AvgNormalTR = (normalTR_TL + normalTR_TR + normalTR_BL + normalTR_BR) * 0.25;

                    SetColor(GradientColor.GetGradientColor(pC.Value, MinE, MaxE), Blend);
                    NormalP3D(AvgNormalC);
                    VertexP3D(pC);

                    SetColor(GradientColor.GetGradientColor(pT.Value, MinE, MaxE), Blend);
                    NormalP3D(AvgNormalT);
                    VertexP3D(pT);

                    SetColor(GradientColor.GetGradientColor(pR.Value, MinE, MaxE), Blend);
                    NormalP3D(AvgNormalR);
                    VertexP3D(pR);

                    SetColor(GradientColor.GetGradientColor(pT.Value, MinE, MaxE), Blend);
                    NormalP3D(AvgNormalT);
                    VertexP3D(pT);

                    SetColor(GradientColor.GetGradientColor(pTR.Value, MinE, MaxE), Blend);
                    NormalP3D(AvgNormalTR);
                    VertexP3D(pTR);

                    SetColor(GradientColor.GetGradientColor(pR.Value, MinE, MaxE), Blend);
                    NormalP3D(AvgNormalR);
                    VertexP3D(pR);
                }
            }
            Gl.glEnd();

            Gl.glEndList();
        }


        protected override void BeginSceneFunction()
        {
            ClearColor(BackgroundColor);

            EnableColor();
            EnableLighting(Light1PositionPoint, Light2PositionPoint);

            //Gl.glEnable(Gl.GL_AUTO_NORMAL);
            Gl.glEnable(Gl.GL_NORMALIZE);

            TranslateToCenter();
            RotateAlongCenter();

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPushMatrix();

            SetScale();
        }

        protected override void EndSceneFunction()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPopMatrix();
        }

        private void SetScale()
        {
            Gl.glScaled(ScaleQ1, ScaleQ2, ScaleE);
        }

        private void SetUnscale()
        {
            Gl.glScaled(1/ScaleQ1, 1/ScaleQ2, 1/ScaleE);
        }

        public void DrawSurface(ISurface3D surface)
        {
            if (RecalcSurface && surface != null)
            {
                CreateSurfaceList(surface);

                RecalcSurface = false;
            }



            SetCurrentPoligoneMode();

            Gl.glCallList(SurfaceList);
            


        }

        public void DrawCurrentPoint(ISurface3D surface)
        {
            if (surface != null && surface.CurrentPoint != null)
            {
                var CurrentPointSpere = CreateNewObject();

                var pCoord = new Point3D(surface.CurrentPoint.X, surface.CurrentPoint.Y, surface.CurrentE);

                if (pCoord.Z > surface.MaxE.Value) pCoord.Z = surface.MaxE.Value;

                pCoord.Z *= GetNormalCoeffE(surface);
                pCoord.X *= GetNormalCoeffQ1(surface, GetQ1Normal(surface));
                pCoord.Y *= GetNormalCoeffQ2(surface, GetQ2Normal(surface));


                Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_FILL);
                SetColor(CurrentPointColor);

                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glPushMatrix();
                TranslateP3D(pCoord);
                SetUnscale();
                Glu.gluSphere(CurrentPointSpere, CurrentPointSize, CurrentPointSlice, CurrentPointStack);
                Gl.glPopMatrix();
                

                DeleteObject(CurrentPointSpere);
            }
        }

        public void DrawPath(ISurface3D surface, IPath path)
        {
            if (surface != null && path != null)
            {
                var NormalCoeffE = GetNormalCoeffE(surface);
                var NormalCoeffQ1 = GetNormalCoeffQ1(surface, GetQ1Normal(surface));
                var NormalCoeffQ2 = GetNormalCoeffQ2(surface, GetQ2Normal(surface));

                SetColor(PathLineColor);
                Gl.glLineWidth(PathLineWidth);

                Gl.glBegin(Gl.GL_LINE_STRIP);

                var LineCount = 1000;

                var step = path.CurrentPointIndex/LineCount + 1;

                var prevCoord = new PointD();

                for (var i = 0; i < path.CurrentPointIndex; i += step)
                {
                    var pCoord = new Point3D(path[i].Point.X, path[i].Point.Y, path[i].Value);

                    if (pCoord.Z > surface.MaxE.Value) pCoord.Z = surface.MaxE.Value;


                    double x = pCoord.X;
                    double y = pCoord.Y;
                    surface.IsValidCoord(ref x, ref y);
                    pCoord.X = x;
                    pCoord.Y = y;


                    pCoord.Z = pCoord.Z * NormalCoeffE + 0.01;
                    pCoord.X *= NormalCoeffQ1;
                    pCoord.Y *= NormalCoeffQ2;

                    if (prevCoord.SquearDistanceTo(pCoord.X, pCoord.Y) > 0.5)
                    {
                        Gl.glEnd();
                        Gl.glBegin(Gl.GL_LINE_STRIP);
                    }

                    VertexP3D(pCoord);

                    prevCoord.SetValues(pCoord.X, pCoord.Y);
                    
                }
                Gl.glEnd();
                
            }
        }

        public void ClearCashe()
        {
            RecalcSurface = true;
        }

        
    }
}
