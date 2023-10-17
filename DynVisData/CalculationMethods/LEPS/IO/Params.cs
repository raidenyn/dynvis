using System.IO;
using DynVis.Core;
using DynVis.Core.IO;
using DynVis.Data.Properties;

namespace DynVis.Data.CalculationMethods.LEPS
{
    partial class ParamsLEPS : ISaveFile
    {
        public void SaveToFile(Stream stream, FileFilter filter)
        {
            //var Serialiser = new SoapFormatter();
            //Serialiser.Serialize(stream, this);

            Serializer.Serialize(this, stream);
        }

        public static ParamsLEPS OpenFromFile(Stream stream, FileFilter filter)
        {
            //var Serialiser = new SoapFormatter();
            //return (ParamsLEPS)Serialiser.Deserialize(stream);

            return Serializer.Deserialize<ParamsLEPS>(stream);
        }

        private static readonly FileFilter[] _SaveFilters = new[] { new FileFilter(LangResource.LEPSParams, "*.leps", FileFilter.Type.Normal) };
        public FileFilter[] SaveFilters
        {
            get { return _SaveFilters; }
        }
    }
}
