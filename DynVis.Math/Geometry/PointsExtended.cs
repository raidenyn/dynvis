using System.Drawing;
using M = System.Math;

namespace DynVis.Math
{
    public static class PointsFunc
    {
        /// <summary>
        /// Погрешность сравнения
        /// </summary>
        public const double Fallibility = 1E-9;

        public static Point3D GetOrdX()
        {
            return new Point3D(1,0,0);
        }

        public static Point3D GetOrdY()
        {
            return new Point3D(0, 1, 0);
        }

        public static Point3D GetOrdZ()
        {
            return new Point3D(0, 0, 1);
        }

        /// <summary>
        /// Проверяет равенство двух векторов
        /// </summary>
        /// <param name="p"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool Equals(this IPointD p, double x, double y)
        {
            return p != null && p.X == x && p.Y == y;
        }

        /// <summary>
        /// Проверяет равенство двух векторов
        /// </summary>
        /// <param name="p"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static bool Equals(this IPoint3D p, double x, double y, double z)
        {
            return p != null && p.X == x && p.Y == y && p.Z == z;
        }

        /// <summary>
        /// Квадрат растояния от вектора между двумя векторами
        /// </summary>
        /// <param name="p1">Первый вектор</param>
        /// <param name="p2">Второй вектор</param>
        /// <returns>Квадрат растояния</returns>
        static public double SquearDistanceTo(this IPointD p1, IPointD p2)
        {
            return p1.SquearDistanceTo(p2.X, p2.Y);
        }

        /// <summary>
        /// Квадрат растояния от вектора между двумя векторами
        /// </summary>
        /// <param name="p1">Первый вектор</param>
        /// <param name="X">Координата X второго вектора</param>
        /// <param name="Y">Координата Н второго вектора</param>
        /// <returns>Квадрат растояния</returns>
        static public double SquearDistanceTo(this IPointD p1, double X, double Y)
        {
            return SquearDistanceTo(p1.X,p1.Y,X,Y);
        }

        /// <summary>
        /// Квадрат растояния от вектора между двумя векторами
        /// </summary>
        /// <param name="X1">Координата X первого вектора</param>
        /// <param name="Y1">Координата Y первого вектора</param>
        /// <param name="X2">Координата X второго вектора</param>
        /// <param name="Y2">Координата Y второго вектора</param>
        /// <returns>Квадрат растояния</returns>
        static public double SquearDistanceTo(double X1, double Y1, double X2, double Y2)
        {
            var DX = (X1 - X2);
            var DY = (Y1 - Y2);
            return DX * DX + DY * DY;
        }

        /// <summary>
        /// Растояние от вектора между двумя векторами
        /// </summary>
        /// <param name="p1">Первый вектор</param>
        /// <param name="p2">Второй вектор</param>
        /// <returns>Растояние</returns>
        static public double DistanceTo(this IPointD p1, IPointD p2)
        {
            return p1.DistanceTo(p2);
        }

        /// <summary>
        /// Растояние от вектора между двумя векторами
        /// </summary>
        /// <param name="p1">Первый вектор</param>
        /// <param name="X">Координата X второго вектора</param>
        /// <param name="Y">Координата Н второго вектора</param>
        /// <returns>Растояние</returns>
        static public double DistanceTo(this IPointD p1, double X, double Y)
        {
            return M.Sqrt(p1.SquearDistanceTo(X, Y));
        }

        /// <summary>
        /// Квадрат растояния от вектора между двумя векторами
        /// </summary>
        /// <param name="p1">Первый вектор</param>
        /// <param name="p2">Второй вектор</param>
        /// <returns>Квадрат растояния</returns>
        static public double SquearDistanceTo(this IPoint3D p1, IPoint3D p2)
        {
            var DX = (p1.X - p2.X);
            var DY = (p1.Y - p2.Y);
            var DZ = (p1.Z - p2.Z);
            return DX * DX + DY * DY + DZ * DZ;
        }

        /// <summary>
        /// Растояние от вектора между двумя векторами
        /// </summary>
        /// <param name="p1">Первый вектор</param>
        /// <param name="p2">Второй вектор</param>
        /// <returns>Растояние</returns>
        static public double DistanceTo(this IPoint3D p1, IPoint3D p2)
        {
            return M.Sqrt(p1.SquearDistanceTo(p2));
        }

        /// <summary>
        /// Квадрат растояния от вектора между двумя векторами
        /// </summary>
        /// <param name="p1">Первый вектор</param>
        /// <param name="p2">Второй вектор</param>
        /// <returns>Квадрат растояния</returns>
        static public double SquearDistanceTo(this Point p1, Point p2)
        {
            var DX = (p1.X - p2.X);
            var DY = (p1.Y - p2.Y);
            return DX * DX + DY * DY;
        }

        /// <summary>
        /// Растояние от вектора между двумя векторами
        /// </summary>
        /// <param name="p1">Первый вектор</param>
        /// <param name="p2">Второй вектор</param>
        /// <returns>Растояние</returns>
        static public double DistanceTo(this Point p1, Point p2)
        {
            return M.Sqrt(p1.SquearDistanceTo(p2));
        }

        /// <summary>
        /// Делит координаты вектора A на число d
        /// </summary>
        /// <param name="A">Вектор</param>
        /// <param name="d">Число</param>
        /// <returns>Результат деления</returns>
        static public Point3D DivideVector3D(IPoint3D A, double d)
        {
            return new Point3D(A.X / d, A.Y / d, A.Z / d);
        }

        /// <summary>
        /// Делит координаты вектора A на число d
        /// </summary>
        /// <param name="A">Вектор</param>
        /// <param name="d">Число</param>
        /// <returns>Результат деления</returns>
        static public void DivideVector3D(Point3D A, double d)
        {
            A.X /= d;
            A.Y /= d;
            A.Z /= d;
        }

        /// <summary>
        /// Сравнивает два вектора равенство с заданной погрешностью
        /// </summary>
        /// <param name="A">Вектор</param>
        /// <param name="B">Вектор</param>
        /// <param name="Epsilon">Погрешность сравнения</param>
        /// <returns>Результат</returns>
        static public bool SimilarlyVector3D(this IPoint3D A, IPoint3D B, double Epsilon)
        {
            bool result = (M.Abs(A.X - B.X) < Epsilon);
            result = result && (M.Abs(A.Y - B.Y) < Epsilon);
            result = result && (M.Abs(A.Z - B.Z) < Epsilon);
            return result;
        }

        /// <summary>
        /// Создает вектор из сферических координат
        /// </summary>
        /// <param name="R">Растояние от центра</param>
        /// <param name="Alpha">Угол между проекцией вектора на плоскость XY и осью X</param>
        /// <param name="Beta">Угол между вектором и осью Z</param>
        /// <returns>Вектор</returns>
        static public Point3D SphereCoord(double R, double Alpha, double Beta)
        {
            return new Point3D(R * M.Sin(Beta) * M.Cos(Alpha), R * M.Sin(Beta) * M.Sin(Alpha), R * M.Cos(Beta));
        }

        /// <summary>
        /// Создает вектор из целендрических координат
        /// </summary>
        /// <param name="R">Растояние от центра</param>
        /// <param name="Alpha">Угол</param>
        /// <param name="Z">Z</param>
        /// <returns>Вектор</returns>
        static public Point3D CylinderCoord(double R, double Alpha, double Z)
        {
            return new Point3D(R * M.Cos(Alpha), R * M.Sin(Alpha), Z);
        }

        /// <summary>
        /// Устанавливает значение вектора из сферических сферических координат
        /// </summary>
        /// <param name="A">Вектор</param>
        /// <param name="R">Растояние от центра</param>
        /// <param name="Alpha">Угол между проекцией вектора на плоскость XY и осью X</param>
        /// <param name="Beta">Угол между вектором и осью Z</param>
        /// <returns>Вектор</returns>
        static public void SetSphereCoord(this Point3D A, double R, double Alpha, double Beta)
        {
            A.X = R * M.Sin(Beta) * M.Cos(Alpha);
            A.Y = R * M.Sin(Beta) * M.Sin(Alpha);
            A.Z = R * M.Cos(Beta);
        }

        /// <summary>
        /// Создает вектор из целендрических координат
        /// </summary>
        /// <param name="A"></param>
        /// <param name="R">Растояние от центра</param>
        /// <param name="Alpha">Угол</param>
        /// <param name="z">Z</param>
        /// <returns>Вектор</returns>
        static public void SetCylinderCoord(this Point3D A, double R, double Alpha, double z)
        {
            A.X = R * M.Cos(Alpha);
            A.Y = R * M.Sin(Alpha);
            A.Z = z;
        }

        /// <summary>
        /// Длина вектора
        /// </summary>
        /// <param name="A"></param>
        /// <returns>Длина вектора</returns>
        static public double Length(this IPoint3D A)
        {
            return System.Math.Sqrt(A.X * A.X + A.Y * A.Y + A.Z * A.Z);
        }

        /// <summary>
        /// Длина вектора
        /// </summary>
        /// <param name="A"></param>
        /// <returns>Длина вектора</returns>
        static public double Length(this IPointD A)
        {
            return System.Math.Sqrt(A.X * A.X + A.Y * A.Y);
        }

        /// <summary>
        /// Нормализует вектор
        /// </summary>
        /// <param name="A"></param>
        static public void Normalize(this Point3D A)
        {
            var K = A.Length();
            if (K != 0)
            {
                DivideVector3D(A, K);
            }
            else
            {
                A.SetValues(0, 0, 1);
            }
        }

        /// <summary>
        /// Создает нормализованный вектор
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        static public Point3D Normalize(this IPoint3D A)
        {
            var result = new Point3D(A);
            result.Normalize();
            return result;
        }


        /// <summary>
        /// Обращает вектор в пространстве
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        static public Point3D Inverse(IPoint3D A)
        {
            return new Point3D(-A.X, -A.Y, -A.Z);
        }

        /// <summary>
        /// Создает вектор противоположенный данному
        /// </summary>
        /// <param name="A"></param>
        static public void Inverse(this Point3D A)
        {
            A.X = -A.X;
            A.Y = -A.Y;
            A.Z = -A.Z;
        }


        /// <summary>
        /// Возращает значение сйерической координаты альфа
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        static public double GetSphereCoordAlpha(this IPoint3D A)
        {
            if (A.X == 0)
            {
                if (A.Y >= 0)
                {
                    return System.Math.PI * 0.5;
                }
                return -System.Math.PI * 0.5;
            }
            return System.Math.Atan2(A.Y, A.X);
        }

        /// <summary>
        /// Возращает значение сйерической координаты бета
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        static public double GetSphereCoordBeta(this IPoint3D A)
        {
            var R = A.Length();
            if (R == 0)
            {
                return 0;
            }
            if (System.Math.Abs(A.Z - R) < Fallibility)
            {
                return 0;
            }
            if (System.Math.Abs(System.Math.Abs(A.Z) - R) < Fallibility)
            {
                return System.Math.PI;
            }
            return System.Math.Acos(A.Z / R);
        }

        /// <summary>
        /// Перобразует вектор в заданный базис
        /// </summary>
        /// <param name="A"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="i2"></param>
        /// <param name="j2"></param>
        /// <param name="k2"></param>
        /// <returns></returns>
        static public Point3D GetVectorInBasis(IPoint3D A, IPoint3D i, IPoint3D j, IPoint3D k,
                                      IPoint3D i2,
                                      IPoint3D j2,
                                      IPoint3D k2)
        {
            return new Point3D(CosAngleVector3D(i2, i) * A.X + CosAngleVector3D(i2, j) * A.Y + CosAngleVector3D(i2, k) * A.Z,
                               CosAngleVector3D(j2, i) * A.X + CosAngleVector3D(j2, j) * A.Y + CosAngleVector3D(j2, k) * A.Z,
                               CosAngleVector3D(k2, i) * A.X + CosAngleVector3D(k2, j) * A.Y + CosAngleVector3D(k2, k) * A.Z);
        }



        /// <summary>
        /// Сумма векторов
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        static public Point3D SummOfVector3D(IPoint3D A, IPoint3D B)
        {
            return new Point3D(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        }

        /// <summary>
        /// Добовляет вектор B к A
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>

        static public void AddVector3D(Point3D A, IPoint3D B)
        {
            A.X += B.X;
            A.Y += B.Y;
            A.Z += B.Z;
        }

        
        /// <summary>
        /// Разность векторов
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        static public Point3D DiffOfVector3D(IPoint3D A, IPoint3D B)
        {
            return new Point3D(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
        }

        /// <summary>
        /// Вычитает из вектора A вектор B
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        static public void SubtVector3D(Point3D A, IPoint3D B)
        {
            A.X -= B.X;
            A.Y -= B.Y;
            A.Z -= B.Z;
        }

        /// <summary>
        /// Вычитает из вектора A вектор B
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        static public Point3D SubtVector3D(IPoint3D A, IPoint3D B)
        {
            return new Point3D(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
        }

        /// <summary>
        /// Вычитает из вектора A вектор B
        /// </summary>
        static public Point3D SubtVector3D(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return new Point3D(x1 - x2, y1 - y2, z1 - z2);
        }

        /// <summary>
        /// Векторной произведение векторов
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        static public Point3D VectMultVector3D(IPoint3D A, IPoint3D B)
        {
            return new Point3D(A.Y * B.Z - B.Y * A.Z, B.X * A.Z - A.X * B.Z, A.X * B.Y - B.X * A.Y);
        }

        /// <summary>
        /// Возращает нормализованую нормаль к плоскости заданую тремя векторами
        /// </summary>
        static public Point3D NormalVector3D(IPoint3D A, IPoint3D B, IPoint3D C)
        {
            var p1 = SubtVector3D(B, A);
            var p2 = SubtVector3D(C, A);

            var normal = VectMultVector3D(p1, p2);

            AddVector3D(normal, A);

            normal.Normalize();

            return normal;
        }


        /// <summary>
        /// Скалярное произведение векторов
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        static public double ScalarMultVector3D(IPoint3D A, IPoint3D B)
        {
            return A.X * B.X + A.Y * B.Y + A.Z * B.Z;
        }
        
        /// <summary>
        /// Смешанное произведение векторов
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        static public double ScalarTripleProductVector3D(IPoint3D A, IPoint3D B, IPoint3D C)
        {
            return A.X * (B.Y * C.Z - B.Z * C.Y) - A.Y * (B.X * C.Z - B.Z * C.X) + A.Z * (B.X * C.Y - B.Y * C.X);
        }
        

        /// <summary>
        /// Находит точку(вектор) делящий вектр AB в соотношении 1:P
        /// При P=1, находится центр масс отрезка
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        static public Point3D PropDivisionVector3D(IPoint3D A, IPoint3D B, double P)
        {
            return new Point3D((A.X + B.X * P) / (1 + P), (A.Y + B.Y * P) / (1 + P), (A.Z + B.Z * P) / (1 + P));
        }

        /// <summary>
        /// Находит точку(вектор) проекции точки C на прямую лежащую на AB
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        static public Point3D ProjectionPointVector3D(IPoint3D A, IPoint3D B, IPoint3D C)
        {
            var AB = DiffOfVector3D(A, B);
            var AC = DiffOfVector3D(A, C);
            var S = AB / AB.Length();
            AB = S * ScalarMultVector3D(AC, S);
            return SummOfVector3D(B, AB);
        }

        /// <summary>
        /// Косинус угла между двумя векторами
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        static public double CosAngleVector3D(IPoint3D A, IPoint3D B)
        {
            double Result = ScalarMultVector3D(A.Normalize(), B.Normalize());
            if (Result > 1)
            {
                Result = 1;
            }
            if (Result < -1)
            {
                Result = -1;
            }
            return (Result);
        }

        /// <summary>
        /// Угол между двумя векторами
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        static public double AngleVector3D(IPoint3D A, IPoint3D B)
        {
            return System.Math.Acos(CosAngleVector3D(A, B));
        }

        /// <summary>
        /// Косинус угла тремя точками
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        static public double CosAnglePoint3D(IPoint3D A, IPoint3D B, IPoint3D C)
        {
            return CosAngleVector3D(DiffOfVector3D(A, B), DiffOfVector3D(B, C));
        }
        
        /// <summary>
        /// Угол между тремя точками
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        static public double AnglePoint3D(IPoint3D A, IPoint3D B, IPoint3D C)
        {
            return AngleVector3D(DiffOfVector3D(B, A), DiffOfVector3D(B, C));
        }
        
        /// <summary>
        /// Диэдральный угол между тремя вектарами
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        static public double DiedralAngleVector3D(IPoint3D A, IPoint3D B, IPoint3D C)
        {
            var vec1 = DiffOfVector3D(A, B);
            var vec2 = DiffOfVector3D(C, B);

            var norm1 = VectMultVector3D(vec1, B);
            var norm2 = VectMultVector3D(vec2, B);

            var Result = AngleVector3D(norm1, norm2);
            if (ScalarMultVector3D(B, VectMultVector3D(norm1, norm2)) > 0)
            {
                Result = Result * -1;
            }
            return Result;
        }

        /// <summary>
        /// Диэдральный угол между четырьмя точками
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <returns></returns>
        static public double DiedralAnglePoint3D(IPoint3D A, IPoint3D B, IPoint3D C, IPoint3D D)
        {
            return DiedralAngleVector3D(DiffOfVector3D(A, B), DiffOfVector3D(C, B), DiffOfVector3D(D, B));
        }




        /// <summary>
        /// Возвращает вектор еденичной длинный C, оборазующий с вектором A
        /// угол Alpha, и угол между плоскостями AC и AB равным Beta
        /// При Right равным true - система векторов правая, при false - левая
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="Alpha"></param>
        /// <param name="Beta"></param>
        /// <returns></returns>
        static public Point3D ZChainNormilizeVector3D(IPoint3D A, IPoint3D B, double Alpha, double Beta)
        {
            //Находим вектор в ортонормированной системе
            var Result = SphereCoord(1, Beta, Alpha);

            //Ищим параметры вращения чтобы вращением преобразовать найденый вектор в целивой
            var Phi1 = A.GetSphereCoordAlpha();
            var Teta = A.GetSphereCoordBeta();

            var vB = new Point3D();

            vB.SetSphereCoord(1, B.GetSphereCoordAlpha() - Phi1, B.GetSphereCoordBeta());
            vB.SetValues(B.X, B.Z, B.Y);

            vB.SetSphereCoord(1, B.GetSphereCoordAlpha() + Teta, B.GetSphereCoordBeta());
            vB.SetValues(B.X, B.Z, B.Y);

            var Phi2 = B.GetSphereCoordAlpha();

            //Вращения вектора
            Result.SetSphereCoord(1, Result.GetSphereCoordAlpha() + Phi2, Result.GetSphereCoordBeta());
            Result.SetValues(Result.X, Result.Z, Result.Y);
            Result.SetSphereCoord(1, Result.GetSphereCoordAlpha() - Teta, Result.GetSphereCoordBeta());
            Result.SetValues(Result.X, Result.Z, Result.Y);
            Result.SetSphereCoord(1, Result.GetSphereCoordAlpha() + Phi1, Result.GetSphereCoordBeta());

            return Result;
        }

 
        /// <summary>
        /// Возвращает точку D, удаленную от А на растояние R,
        /// отрезок DA оборазует с отрезком AB угол угол Alpha
        /// Угол между плоскостями ВAB и DAC равным Beta
        /// При Right равным true - система векторов правая, при false - левая
        /// </summary>
        /// <param name="A"></param>
        /// <param name="R"></param>
        /// <param name="B"></param>
        /// <param name="Alpha"></param>
        /// <param name="C"></param>
        /// <param name="Beta"></param>
        /// <returns></returns>
        static public Point3D ZChainNormilizePoint3D(IPoint3D A,
                                         double R,
                                         IPoint3D B,
                                         double Alpha,
                                         IPoint3D C,
                                         double Beta)
        {
            var vB = DiffOfVector3D(B, A);
            var vC = DiffOfVector3D(C, A);
            return SummOfVector3D(ZChainNormilizeVector3D(vB, vC, Alpha, Beta) * R, A);
        }

        /// <summary>
        /// Устанавливает вектор в положительное направление
        /// </summary>
        /// <param name="A"></param>
        static public void SetPositiveDirection(this Point3D A)
        {
            if (A.Z < 0)
            {
                A.Inverse();
            }
            else
            {
                if ((A.Z == 0) && (A.Y < 0))
                {
                    A.Inverse();
                }
                else
                {
                    if ((A.Y == 0) && (A.X < 0))
                    {
                        A.Inverse();
                    }
                }
            }
        }


    }
}
