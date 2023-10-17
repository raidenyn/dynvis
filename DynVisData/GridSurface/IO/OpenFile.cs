using System;
using DynVis.Core;
using DynVis.Core.IO;
using DynVis.Core.Surface;
using DynVis.Data.Properties;

namespace DynVis.Data.GridSurface
{
    static class OpenFile
    {
        public static string FromFile(out double[] Q1, out double[] Q2, out double[,] Energy, out Axes axes1, out Axes axes2)
        {
            Q1 = Q2 = null;
            Energy = null;
            axes1 = new Axes();
            axes2 = new Axes();

            var source = IOFileDialog.OpenTextFile(FileFilters);
            if (!String.IsNullOrEmpty(source))
            {
                var ObjectTextReader = new ObjectTextReader(source);

                if (!ObjectTextReader.ReadObject(BaseSurface3D.Axes1ValuesName, axes1))
                    return LangResource.ReadErrorQ1Axes;
                if (!ObjectTextReader.ReadObject(BaseSurface3D.Axes2ValuesName, axes2))
                    return LangResource.ReadErrorQ2Axes;


                Q1 = ObjectTextReader.ReadArray<double>(BaseSurface3D.Q1ValuesName, ObjectTextReader.Separators,
                                                        ObjectTextReader.EndSectionPattern);
                if (Q1 == null) return LangResource.ReadErrorQ1Knots;
                Q2 = ObjectTextReader.ReadArray<double>(BaseSurface3D.Q2ValuesName, ObjectTextReader.Separators,
                                                        ObjectTextReader.EndSectionPattern);
                if (Q2 == null) return LangResource.ReadErrorQ2Knots;
                Energy = ObjectTextReader.ReadArray2D<double>(BaseSurface3D.EnergyValuesName,
                                                              ObjectTextReader.Separators,
                                                              ObjectTextReader.EndSectionPattern, Q1.Length, Q2.Length);
                
                
                return (Energy != null) ? String.Empty : LangResource.ReadErrorEnergyArray;
            } 

            return null;
        }

        public static readonly FileFilter[] FileFilters = new[] {new FileFilter(LangResource.ApplicateArray, "*.sam")};
    }
}
