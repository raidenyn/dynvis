using M=System.Math;

namespace DynVis.Math.Geometry
{
    public static class TransformationMatrixExtension
    {
        /// <summary>
        /// Перемещает систему координат в точку p
        /// </summary>
        /// <param name="p">Новое начало координат</param>
        /// <returns></returns>
        public static TransformationMatrix MoveTo(IPoint3D p)
        {
            return TransformationMatrix.GetTranslate(-p.X, -p.Y, -p.Z);
        }

        /// <summary>
        /// Направляет ось Z в точку p.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static TransformationMatrix DirectZTo(IPoint3D p)
        {
            TransformationMatrix result;

            //Находим полную длину вектора
            var length = p.Length();

            //Находим длину проекции на плоскость XY
            var d = ((IPointD)p).Length();

            if (d != 0) //Если длина отлична от нуля, то... 
            {
                //...находим угол между проекцией и осью X
                var c = p.X / d;
                var s = p.Y / d;

                //И поворачиваем на этот угол против часовой (у синуса меняется знак)
                result = TransformationMatrix.GetRotateZ(-s, c);
            } 
            else //Проекция равна нулю мы уже на оси Z (но может быть смотрим в другую сторну)
            {
                result = new TransformationMatrix();
            }

            //Поворачиваем по оси Y на угол между прямой и осью Z против часовой стрелки
            result *= TransformationMatrix.GetRotateY(-d / length, p.Z / length);

            //Получаем совпадение оси Z с заданным вектором.
            return result;
        }

        /// <summary>
        /// Вращает систему вокруг вектора p.
        /// </summary>
        /// <param name="p">Ось вращения</param>
        /// <param name="angle">Угол вращения</param>
        /// <returns></returns>
        public static TransformationMatrix Rotate(IPoint3D p, double angle)
        {
            var direct = DirectZTo(p);
            var rotate = TransformationMatrix.GetRotateZ(angle);
            var undirect = TransformationMatrix.InverseMatrix(direct);

            return direct*rotate*undirect;
        }

        /// <summary>
        /// Совмещает направление вектора source с направлением вектора deriection
        /// </summary>
        /// <param name="source"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static TransformationMatrix Superpose(IPoint3D source, IPoint3D direction)
        {
            var norm = PointsFunc.VectMultVector3D(source, direction);

            var angle = PointsFunc.AngleVector3D(source, direction);

            return Rotate(norm, -angle);
        }

        /// <summary>
        /// Устанавливаем стандартную ориентацию системы по трем точкам
        /// </summary>
        /// <param name="ceneter">Центер системы</param>
        /// <param name="zDirection">Направление оси Z</param>
        /// <param name="xzOrientation">Определяет плоскость ZX</param>
        /// <returns></returns>
        public static TransformationMatrix StandartOrientation(IPoint3D ceneter, IPoint3D zDirection, IPoint3D xzOrientation)
        {
            //Устанавливаем центер
            var toCeneter = MoveTo(ceneter);
            
            var zDirectionPoint = toCeneter.ApplyTo(zDirection);

            //Напряалвяем ось Z
            var zDirect = DirectZTo(zDirectionPoint);
            
            //Находим результат перемножением матриц
            var result = toCeneter*zDirect;
            
            var xzOrientationPoint = result.ApplyTo(xzOrientation);

            var d = ((IPointD)xzOrientationPoint).Length();
            if (d != 0) //Если xzOrientationPoint не совпало с плоскостью XZ...
            {
                var c = xzOrientationPoint.X / d;
                var s = xzOrientationPoint.Y / d;

                //... то поворачиваем 
                var rotateXZ = TransformationMatrix.GetRotateZ(-s, c);

                return result * rotateXZ;
            }
            
            return result;
        }
    }
}
