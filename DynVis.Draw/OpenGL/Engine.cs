using M=System.Math;
using System.Collections.Generic;
using System.Drawing;
using DynVis.Core;
using DynVis.Math;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace DynVis.Draw.OpenGL
{
    public abstract partial class OpenGLEngine
    {
        private static readonly float[] mat_ambient = new[] { 0.5f, 0.5f, 0.5f, 1.0f };
        private readonly static float[] mat_specular = new[] { 1.0f, 1.0f, 1.0f, 1.0f };
        private readonly static float[] mat_shininess = new[] { 50.0f };
        private readonly static float[] model_ambient = new[] { 0.5f, 0.5f, 0.5f, 1.0f };


        public readonly static float[] light1_ambient = new[] { 0.0f, 0.0f, 0.0f, 1.0f };
        public readonly static float[] light1_diffuse = new[] { 0.8f, 0.8f, 0.8f, 0.8f };
        public readonly static float[] light1_specular = new[] { 1.0f, 1.0f, 1.0f, 1.0f };
        private readonly static float[] light1_position = new[] { 1.0f, 1.0f, 1.0f, 0.0f };

        public readonly static float[] light2_ambient = new[] { 0.0f, 0.0f, 0.0f, 0.0f };
        public readonly static float[] light2_diffuse = new[] { 0.1f, 0.1f, 0.1f, 0.1f };
        public readonly static float[] light2_specular = new[] { 0.3f, 0.3f, 0.3f, 0.3f };
        private readonly static float[] light2_position = new[] { -2.0f, -2.0f, 1.0f, 1.0f };

        public float Near = 1.0f;
        public float Fare = 150.0f;

        public static bool UseMultisample = GlobalParams.GetBoolean("UseMultisample", true);
        public static bool UseArbMultisample = GlobalParams.GetBoolean("UseArbMultisample", true);
        public static bool UseShadeSmooth = GlobalParams.GetBoolean("UseShadeSmooth", true);
        public static bool UseBlend = GlobalParams.GetBoolean("UseBlend", true);
        public static bool UseSmooth = GlobalParams.GetBoolean("UseSmooth", false);
        public static bool UseDither = GlobalParams.GetBoolean("UseDither", true);

        private static void SetInitPatams()
        {

            if (UseMultisample)
            {
                Gl.glEnable(Gl.GL_MULTISAMPLE);
            } 
            else
            {
                Gl.glDisable(Gl.GL_MULTISAMPLE);
            }

            if (UseArbMultisample)
            {
                Gl.glEnable(Gl.GL_MULTISAMPLE_ARB);
            }
            else
            {
                Gl.glDisable(Gl.GL_MULTISAMPLE_ARB);
            }

            if (UseShadeSmooth)
            {
                Gl.glShadeModel(Gl.GL_SMOOTH);
            }
            else
            {
                Gl.glShadeModel(Gl.GL_FLAT);
            }


            if (UseBlend)
            {
                Gl.glEnable(Gl.GL_BLEND);
                Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            }
            else
            {
                Gl.glDisable(Gl.GL_BLEND);
            }


            if (UseSmooth)
            {
                Gl.glEnable(Gl.GL_POINT_SMOOTH);
                Gl.glHint(Gl.GL_POINT_SMOOTH_HINT, Gl.GL_NICEST);
                Gl.glEnable(Gl.GL_LINE_SMOOTH);
                Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
                Gl.glEnable(Gl.GL_POLYGON_SMOOTH);
                Gl.glHint(Gl.GL_POINT_SMOOTH_HINT, Gl.GL_NICEST);
            }
            else
            {
                Gl.glDisable(Gl.GL_POINT_SMOOTH);
                Gl.glHint(Gl.GL_POINT_SMOOTH_HINT, Gl.GL_FASTEST);
                Gl.glDisable(Gl.GL_LINE_SMOOTH);
                Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_FASTEST);
                Gl.glDisable(Gl.GL_POLYGON_SMOOTH);
                Gl.glHint(Gl.GL_POINT_SMOOTH_HINT, Gl.GL_FASTEST);
            }



            if (UseDither)
            {
                Gl.glEnable(Gl.GL_DITHER);
            }
            else
            {
                Gl.glDisable(Gl.GL_DITHER);
            }

            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glColorMaterial(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE);

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glDepthFunc(Gl.GL_LESS);
        }

        public void Resize(int Width, int Height)
        {
            if (MakeCurentContext())
            {
                Width = M.Abs(Width);
                Height = M.Abs(Height);
                if (Width == 0)
                {
                    Width = 1;
                }
                if (Height == 0)
                {
                    Height = 1;
                }
                Gl.glViewport(0, 0, Width, Height);
                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glLoadIdentity();
                Glu.gluPerspective(45, Width / (double)Height, Near, Fare);
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glLoadIdentity();
            }
        }

        public bool BeginScene()
        {
            if (MakeCurentContext())
            {
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glLoadIdentity();

                BeginSceneFunction();
                return true;
            }
            return false;
        }

        public void EndScene()
        {
            EndSceneFunction();

            Gdi.SwapBuffers(DC);
        }

        protected abstract void BeginSceneFunction();
        protected abstract void EndSceneFunction();

        public void Refresh()
        {
            if (BeginScene())
            {
                EndScene();
            }
        }

        protected static void DisableLighting()
        {
            Gl.glDisable(Gl.GL_LIGHTING);
        }


        protected static void EnableLighting(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT, mat_ambient);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, mat_specular);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, mat_shininess);

            Gl.glLightModelfv(Gl.GL_LIGHT_MODEL_AMBIENT, model_ambient);

            Gl.glEnable(Gl.GL_LIGHTING);



            light1_position[0] = x1;
            light1_position[1] = y1;
            light1_position[2] = z1;

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, light1_ambient);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, light1_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, light1_specular);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light1_position);

            Gl.glEnable(Gl.GL_LIGHT0);

            
            light2_position[0] = x2;
            light2_position[1] = y2;
            light2_position[2] = z2;

            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, light2_ambient);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, light2_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPECULAR, light2_specular);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, light2_position);

            Gl.glEnable(Gl.GL_LIGHT1);
          
        }

        protected static void ClearColor(Color c)
        {
            Gl.glClearColor(c.R/255.0f, c.G/255.0f, c.B/255.0f, c.A/255.0f);
        }

        protected static void EnableLighting(IPoint3D light1Pos, IPoint3D light2Pos)
        {
            EnableLighting((float)light1Pos.X, (float)light1Pos.Y, (float)light1Pos.Z, (float)light2Pos.X, (float)light2Pos.Y, (float)light2Pos.Z);
        }

        protected static void EnableColor()
        {
            Gl.glEnable(Gl.GL_COLOR);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
        }

        protected List<Glu.GLUquadric> QuadricList = new List<Glu.GLUquadric>();

        protected Glu.GLUquadric CreateNewObject()
        {
            var obj = Glu.gluNewQuadric();

            Glu.gluQuadricNormals(obj, Glu.GLU_SMOOTH);

            QuadricList.Add(obj);

            return obj;
        }

        protected void DeleteObject(Glu.GLUquadric obj)
        {
            Glu.gluDeleteQuadric(obj);
            QuadricList.Remove(obj);
        }

        public static double[] CreateIndentyMatrixd(int n)
        {
            var Result = new double[n*n];
            for (var i = 0; i < n; i++)
            {
                Result[n*i + i] = 1;
            }
            return Result;
        }

        public static float[] CreateIndentyMatrixf(int n)
        {
            var Result = new float[n * n];
            for (var i = 0; i < n; i++)
            {
                Result[n * i + i] = 1;
            }
            return Result;
        }

        public static void TranslateP3D(IPoint3D p)
        {
            TranslateP3D(p, TranslateType.Froward);
        }

        public static void TranslateP3D(IPoint3D p, TranslateType tt)
        {
            if (tt == TranslateType.Froward)
            {
                Gl.glTranslated(p.X, p.Y, p.Z);
            }
            else
            {
                Gl.glTranslated(-p.X, -p.Y, -p.Z);
            }
        }

        public static void RotateAlongAxesd(double angle, Axes axes)
        {
            switch (axes)
            {
                case Axes.X:
                    Gl.glRotated(angle, 1, 0, 0);
                    break;
                case Axes.Y:
                    Gl.glRotated(angle, 0, 1, 0);
                    break;
                case Axes.Z:
                    Gl.glRotated(angle, 0, 0, 1);
                    break;
            }
        }

        public static void RotateByAxesd(double angleX, double angleY, double angleZ)
        {
            Gl.glRotated(angleX, 1, 0, 0);
            Gl.glRotated(angleY, 0, 1, 0);
            Gl.glRotated(angleZ, 0, 0, 1);
        }

        public static void VertexP3D(IPoint3D p)
        {
            Gl.glVertex3d(p.X, p.Y, p.Z);
        }

        public static void NormalP3D(IPoint3D p)
        {
            Gl.glNormal3d(p.X, p.Y, p.Z);
        }

        public void SetRotateMatrix(float[] RotateMatrix, double angle, Axes axes)
        {
            Gl.glPushMatrix();
            Gl.glLoadIdentity();

            RotateAlongAxesd(angle, axes);
            Gl.glMultMatrixf(RotateMatrix);
            
            Gl.glGetFloatv(Gl.GL_MODELVIEW_MATRIX, RotateMatrix);

            Gl.glPopMatrix();
        }

        public void SetColor(Color color)
        {
            Gl.glColor3f(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
        }

        public void SetColor(Color color, float alpha)
        {
            Gl.glColor4f(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, alpha);
        }

        public void TranslateDirectAxesZToPoint(IPoint3D pCenter, IPoint3D pDirect)
        {
            TranslateDirectAxesZToPoint(pCenter.X, pCenter.Y, pCenter.Z, pDirect.X, pDirect.Y, pDirect.Z);
        }

        public void TranslateDirectAxesZToPoint(IPoint3D pCenter, double directX, double directY, double directZ)
        {
            TranslateDirectAxesZToPoint(pCenter.X, pCenter.Y, pCenter.Z, directX, directY, directZ);
        }

        public void TranslateDirectAxesZToPoint(double centerX, double centerY, double centerZ, double directX, double directY, double directZ)
        {
            Gl.glTranslated(centerX, centerY, centerZ);

            var x = directX - centerX;
            var y = directY - centerY;
            var z = directZ - centerZ;

            var a = MathExtended.RadToDeg(M.Atan2(y, x)); //Угол между проекции вектора на плоскость XY и осью X
            var b = MathExtended.RadToDeg(M.Atan2(M.Sqrt(x * x + y * y), z)); //Угол между вектором и осью Z

            //Поворачиваем до совмещения оси x с проекцией вектора на плоскость XY
            Gl.glRotated(a, 0, 0, 1);
            //Поворачиваем до совмещения оси Z с вектором
            Gl.glRotated(b, 0, 1, 0);
            //Востанавливаем первый поворот (нужен для определенности работы алгоритма)
            Gl.glRotated(-a, 0, 0, 1);
        }

        private readonly float[] DirectAxesZToCamera_Buffers = new float[16];
        public void DirectAxesZToCamera()
        {
            lock (DirectAxesZToCamera_Buffers)
            {
                //Получаем матрицу модельных преобразований
                Gl.glGetFloatv(Gl.GL_MODELVIEW_MATRIX, DirectAxesZToCamera_Buffers);

                //Получаем позиции начала координат (значение трансляционных членов матрицы)
                var x = DirectAxesZToCamera_Buffers[12];
                var y = DirectAxesZToCamera_Buffers[13];
                var z = DirectAxesZToCamera_Buffers[14];

                //Сбрасываем матрицу, устанавливаем позицию на исходные позиции (при этом сбрасываются углы)
                //и направляем ось z на камеру
                Gl.glLoadIdentity();
                TranslateDirectAxesZToPoint(x, y, z, 0, 0, 0);
            }
        }

        protected static int FindTopObject(int[] selectBuffer, int objCount)
        {
            var minDepth = selectBuffer[1];
            var objName = selectBuffer[3];
            for (var i = 4; i < objCount*4; i += 4)
            {
                if (selectBuffer[i + 1] < minDepth)
                {
                    minDepth = selectBuffer[i + 1];
                    objName = selectBuffer[i+3];
                }
            }
            return objName;
        }
    }

    public enum TranslateType
    {
        Froward,
        Revers
    } ;

    public enum Axes
    {
        X,
        Y,
        Z
    } ;
}
