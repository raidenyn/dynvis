#region

using System;
using System.Data;
using System.IO;
using DevAge.IO;

#endregion

namespace DevAge.Data
{
    /// <summary>
    /// A static class used to serialize a DataSet to and from a stream using a binary or xml format.
    /// The xml format use the standard DataSet xml serialization, the binary format use a custom format.
    /// </summary>
    public static class StreamDataSet
    {
        private const int c_BinaryVersion = 1;

        /// <summary>
        /// Write the dataset to the stream using the specified format.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="format"></param>
        public static void Write(Stream destination, DataSet source, StreamDataSetFormat format)
        {
            switch (format)
            {
                case StreamDataSetFormat.XML:
                    source.WriteXml(destination, XmlWriteMode.WriteSchema);
                    break;
                case StreamDataSetFormat.Binary:
                    BinWriteDataSetToStream(destination, source);
                    break;
                default:
                    throw new ApplicationException("StreamDataSet Format not supported");
            }
        }

        /// <summary>
        /// Read from the stream and populate the dataset using the specified format.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="format"></param>
        /// <param name="mergeSchema">True to merge the schema, otherwise it is used the schema of the dataset</param>
        public static void Read(Stream source, DataSet destination, StreamDataSetFormat format, bool mergeSchema)
        {
            bool oldEnforce = destination.EnforceConstraints;
            destination.EnforceConstraints = false;
            try
            {
                switch (format)
                {
                    case StreamDataSetFormat.XML:
                        if (mergeSchema)
                            destination.ReadXml(source, XmlReadMode.ReadSchema);
                        else
                            destination.ReadXml(source, XmlReadMode.IgnoreSchema);
                        break;
                    case StreamDataSetFormat.Binary:
                        BinReadDataSetFromStream(source, destination, mergeSchema);
                        break;
                    default:
                        throw new ApplicationException("StreamDataSet Format not supported");
                }
            }
            finally
            {
                destination.EnforceConstraints = oldEnforce;
            }
        }

        private static void BinWriteDataSetToStream(Stream stream, DataSet ds)
        {
            //Version
            StreamPersistence.Write(stream, c_BinaryVersion);

            //Schema byte[]
            byte[] bytesSchema;
            using (var schemaStream = new MemoryStream())
            {
                ds.WriteXmlSchema(schemaStream);
                schemaStream.Flush();
                schemaStream.Seek(0, SeekOrigin.Begin);
                bytesSchema = schemaStream.ToArray();
            }
            StreamPersistence.Write(stream, bytesSchema);

            //Tables
            for (int iTable = 0; iTable < ds.Tables.Count; iTable++)
            {
                DataTable table = ds.Tables[iTable];
                //Only the current Rows
                DataRow[] rows = table.Select(null, null, DataViewRowState.CurrentRows);
                StreamPersistence.Write(stream, rows.Length);
                //Rows
                for (int r = 0; r < rows.Length; r++)
                {
                    //Columns
                    for (int c = 0; c < table.Columns.Count; c++)
                        BinWriteFieldToStream(stream, rows[r][c], table.Columns[c].DataType);
                }
            }
        }

        private static void BinReadDataSetFromStream(Stream stream, DataSet ds, bool mergeSchema)
        {
            //Version
            int version;
            version = StreamPersistence.ReadInt32(stream);

            if (version != c_BinaryVersion)
                throw new BinaryDataSetVersionException();

            //Schema byte[]
            DataSet schemaDS; //Questo dataset viene usato solo per leggere lo schema
            byte[] byteSchema = StreamPersistence.ReadByteArray(stream);
            using (var schemaStream = new MemoryStream(byteSchema))
            {
                if (mergeSchema)
                {
                    ds.ReadXmlSchema(schemaStream);
                    schemaDS = ds;
                }
                else
                {
                    schemaDS = new DataSet();
                    schemaDS.ReadXmlSchema(schemaStream);
                }
            }

            //Tables
            for (int iTable = 0; iTable < schemaDS.Tables.Count; iTable++)
            {
                DataTable schemaTable = schemaDS.Tables[iTable];
                //Table exist on the destination
                if (ds.Tables.Contains(schemaTable.TableName))
                {
                    DataTable destinationTable = ds.Tables[schemaTable.TableName];

                    int rowsCount = StreamPersistence.ReadInt32(stream);
                    //Rows
                    for (int r = 0; r < rowsCount; r++)
                    {
                        DataRow row = destinationTable.NewRow();
                        //Columns
                        for (int c = 0; c < schemaTable.Columns.Count; c++)
                        {
                            string colName = schemaTable.Columns[c].ColumnName;
                            object val = BinReadFieldFromStream(stream, schemaTable.Columns[c].DataType);
                                //Il valore viene comunque letto per far avanzare lo stream

                            if (destinationTable.Columns.Contains(colName))
                                row[colName] = val;
                        }

                        destinationTable.Rows.Add(row);
                    }
                }
                else
                    //Note: if the table not exist I will read anyway the columns and rows to correctly position the stream on the next table
                {
                    int rowsCount = StreamPersistence.ReadInt32(stream);
                    //Rows
                    for (int r = 0; r < rowsCount; r++)
                    {
                        //Columns
                        for (int c = 0; c < schemaTable.Columns.Count; c++)
                        {
                            BinReadFieldFromStream(stream, schemaTable.Columns[c].DataType);
                                //Il valore viene comunque letto per far avanzare lo stream
                        }
                    }
                }
            }

            ds.AcceptChanges();
        }

        private static void BinWriteFieldToStream(Stream stream, object val, Type columnType)
        {
            //Field Status
            if (val == null) //Null
                StreamPersistence.Write(stream, (byte) 0);
            else if (val == DBNull.Value) //DbNull
                StreamPersistence.Write(stream, (byte) 1);
            else
            {
                StreamPersistence.Write(stream, (byte) 2);

                //Field Value
                StreamPersistence.Write(stream, columnType, val);
            }
        }

        private static object BinReadFieldFromStream(Stream stream, Type columnType)
        {
            //Field Status
            byte bt = StreamPersistence.ReadByte(stream);

            if (bt == 0) //Null
                return null;
            else if (bt == 1) //DbNull
                return DBNull.Value;
            else if (bt == 2) //Value
                //Field Value
                return StreamPersistence.Read(stream, columnType);
            else
                throw new BinaryDataSetInvalidException();
        }
    }

    /// <summary>
    /// Enum to control the serialization format
    /// </summary>
    public enum StreamDataSetFormat
    {
        /// <summary>
        /// Standard xml format used by the ReadXml and WriteXml of the DataSet 
        /// </summary>
        XML = 1,
        /// <summary>
        /// Custom binary format. More compact of the xml but not human readable
        /// </summary>
        Binary = 2
    }

    /// <summary>
    /// Binary data not valid exception
    /// </summary>
    [Serializable]
    public class BinaryDataSetInvalidException : DevAgeApplicationException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryDataSetInvalidException() :
            base("Binary data not valid")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_InnerException"></param>
        public BinaryDataSetInvalidException(Exception p_InnerException) :
            base("Binary data not valid", p_InnerException)
        {
        }
    }

    /// <summary>
    /// Version not valid exception
    /// </summary>
    [Serializable]
    public class BinaryDataSetVersionException : DevAgeApplicationException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryDataSetVersionException() :
            base("Binary data version not valid")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_InnerException"></param>
        public BinaryDataSetVersionException(Exception p_InnerException) :
            base("Binary data version not valid", p_InnerException)
        {
        }
    }
}