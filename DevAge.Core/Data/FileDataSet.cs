#region

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

#endregion

namespace DevAge.Data
{
    /// <summary>
    /// Summary description for Manager.
    /// </summary>
    public abstract class FileDataSet
    {
        #region Instance Methdos and Propertis

        private string m_FileName;
        private StreamDataSetFormat mFileDataFormat;
        private StreamDataSetFormat mSaveDataFormat = StreamDataSetFormat.Binary;

        /// <summary>
        /// Gets or Sets the format used to save the DataSet
        /// </summary>
        protected StreamDataSetFormat SaveDataFormat
        {
            get { return mSaveDataFormat; }
            set { mSaveDataFormat = value; }
        }

        /// <summary>
        /// Gets the current format of the File where the data are loaded.
        /// </summary>
        protected StreamDataSetFormat FileDataFormat
        {
            get { return mFileDataFormat; }
        }

        /// <summary>
        /// Gets or Sets if merge the schema of the file with the schema of the DataSet specified. Use true if the DataSet doesn't have a schema definition. Default is false.
        /// </summary>
        protected bool MergeReadedSchema { get; set; }


        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        protected abstract int GetDataVersion();
        protected abstract DataSet CreateData(int version);

        #endregion

        #region Load /Save

        private const string c_DataFormat = "dataformat";
        private const string c_DataNamespace = "http://www.devage.com/FileDataSet";
        private const string c_DataVersion = "dataversion";
        private const string c_FileVersion = "fileversion";
        private const int c_FileVersionNumber = 1;

        protected virtual void SaveToFile(DataSet pDataSet)
        {
            if (m_FileName == null)
                throw new ApplicationException("FileName is null");

            byte[] completeByteArray;
            using (var fileMemStream = new MemoryStream())
            {
                var xmlWriter = new XmlTextWriter(fileMemStream, Encoding.UTF8);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("filedataset", c_DataNamespace);

                xmlWriter.WriteStartElement("header", c_DataNamespace);
                //File Version
                xmlWriter.WriteAttributeString(c_FileVersion, c_FileVersionNumber.ToString());
                //Data Version
                xmlWriter.WriteAttributeString(c_DataVersion, GetDataVersion().ToString());
                //Data Format
                xmlWriter.WriteAttributeString(c_DataFormat, ((int) mSaveDataFormat).ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("data", c_DataNamespace);

                byte[] xmlByteArray;
                using (var xmlMemStream = new MemoryStream())
                {
                    StreamDataSet.Write(xmlMemStream, pDataSet, mSaveDataFormat);
                    //pDataSet.WriteXml(xmlMemStream);

                    xmlByteArray = xmlMemStream.ToArray();
                    xmlMemStream.Close();
                }

                xmlWriter.WriteBase64(xmlByteArray, 0, xmlByteArray.Length);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();

                completeByteArray = fileMemStream.ToArray();
                fileMemStream.Close();
            }

            //se tutto è andato a buon fine scrivo effettivamente il file
            using (var fileStream = new FileStream(m_FileName, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(completeByteArray, 0, completeByteArray.Length);
                fileStream.Close();
            }
        }


        protected virtual DataSet LoadFromFile()
        {
            if (m_FileName == null)
                throw new ApplicationException("FileName is null");

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(m_FileName);

            var namespaceMan = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceMan.AddNamespace("fileds", c_DataNamespace);

            XmlNode tmp = xmlDoc.DocumentElement.SelectSingleNode("fileds:header", namespaceMan);
            if (tmp == null)
                throw new ApplicationException("File header not found");
            var header = (XmlElement) tmp;

            //File Version
            string strFileVersion = header.GetAttribute(c_FileVersion);
            int fileVersion = int.Parse(strFileVersion);
            if (fileVersion == c_FileVersionNumber)
            {
                string strDataFormat = header.GetAttribute(c_DataFormat);
                mFileDataFormat = (StreamDataSetFormat) int.Parse(strDataFormat);
            }
            else if (fileVersion == 0)
            {
                mFileDataFormat = StreamDataSetFormat.XML;
            }
            else if (fileVersion > c_FileVersionNumber)
                throw new ApplicationException("File Version not supported, expected: " + c_FileVersionNumber);

            //Data Version
            string strDataVersion = header.GetAttribute(c_DataVersion);
            int dataVersion = int.Parse(strDataVersion);
            DataSet dataSet = CreateData(dataVersion);

            //Data
            tmp = xmlDoc.DocumentElement.SelectSingleNode("fileds:data", namespaceMan);
            if (tmp == null)
                throw new ApplicationException("File data not found");
            var xmlDataNode = (XmlElement) tmp;

            byte[] xmlBuffer = Convert.FromBase64String(xmlDataNode.InnerText);
            using (var memStream = new MemoryStream(xmlBuffer))
            {
                StreamDataSet.Read(memStream, dataSet, mFileDataFormat, MergeReadedSchema);
                //dataSet.ReadXml(memStream);
            }

            return dataSet;
        }

        #endregion
    }
}