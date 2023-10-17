#region

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

#endregion

namespace DevAge.IO
{
    /// <summary>
    /// A static class to help save and read stream data
    /// </summary>
    public static class StreamPersistence
    {
        #region Write Function

        public static void Write(Stream p_Stream, String p_Value, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(p_Value);
            Write(p_Stream, bytes);
        }

        public static void Write(Stream p_Stream, Byte p_Value)
        {
            p_Stream.WriteByte(p_Value);
        }

        public static void Write(Stream p_Stream, Guid p_Value)
        {
            byte[] vBits = p_Value.ToByteArray();
            Write(p_Stream, vBits);
        }

        public static void Write(Stream p_Stream, Decimal p_Value)
        {
            int[] vBits = Decimal.GetBits(p_Value);
            Write(p_Stream, vBits[0]);
            Write(p_Stream, vBits[1]);
            Write(p_Stream, vBits[2]);
            Write(p_Stream, vBits[3]);
        }

        public static void Write(Stream p_Stream, DateTime p_Value)
        {
            Write(p_Stream, p_Value.ToOADate());
        }

        public static void Write(Stream p_Stream, Int16 p_Value)
        {
            WriteBytes(p_Stream, BitConverter.GetBytes(p_Value));
        }

        public static void Write(Stream p_Stream, Int32 p_Value)
        {
            WriteBytes(p_Stream, BitConverter.GetBytes(p_Value));
        }

        public static void Write(Stream p_Stream, Int64 p_Value)
        {
            WriteBytes(p_Stream, BitConverter.GetBytes(p_Value));
        }

        public static void Write(Stream p_Stream, Single p_Value)
        {
            WriteBytes(p_Stream, BitConverter.GetBytes(p_Value));
        }

        public static void Write(Stream p_Stream, Double p_Value)
        {
            WriteBytes(p_Stream, BitConverter.GetBytes(p_Value));
        }

        public static void Write(Stream p_Stream, Char p_Value)
        {
            WriteBytes(p_Stream, BitConverter.GetBytes(p_Value));
        }

        public static void Write(Stream p_Stream, Boolean p_Value)
        {
            WriteBytes(p_Stream, BitConverter.GetBytes(p_Value));
        }

        //[CLSCompliant(false)]
        public static void Write(Stream p_Stream, UInt16 p_Value)
        {
            WriteBytes(p_Stream, BitConverter.GetBytes(p_Value));
        }

        //[CLSCompliant(false)]
        public static void Write(Stream p_Stream, UInt32 p_Value)
        {
            WriteBytes(p_Stream, BitConverter.GetBytes(p_Value));
        }

        //[CLSCompliant(false)]
        public static void Write(Stream p_Stream, UInt64 p_Value)
        {
            WriteBytes(p_Stream, BitConverter.GetBytes(p_Value));
        }

        public static void Write(Stream p_Stream, Byte[] p_Bytes)
        {
            Write(p_Stream, p_Bytes.Length);
            WriteBytes(p_Stream, p_Bytes);
        }


        public static void Write(Stream stream, Type valueType, object val)
        {
            if (valueType == typeof (String))
                Write(stream, (String) val, Encoding.UTF8);
            else if (valueType == typeof (Int16))
                Write(stream, (Int16) val);
            else if (valueType == typeof (Int32))
                Write(stream, (Int32) val);
            else if (valueType == typeof (Int64))
                Write(stream, (Int64) val);
            else if (valueType == typeof (Single))
                Write(stream, (Single) val);
            else if (valueType == typeof (Double))
                Write(stream, (Double) val);
            else if (valueType == typeof (Boolean))
                Write(stream, (Boolean) val);
            else if (valueType == typeof (Decimal))
                Write(stream, (Decimal) val);
            else if (valueType == typeof (Char))
                Write(stream, (Char) val);
            else if (valueType == typeof (Byte))
                Write(stream, (Byte) val);
            else if (valueType == typeof (Byte[]))
                Write(stream, (Byte[]) val);
            else if (valueType == typeof (DateTime))
                Write(stream, (DateTime) val);
            else if (valueType == typeof (Guid))
                Write(stream, (Guid) val);
            else
                throw new TypeNotSupportedException(valueType);
        }

        public static void WriteBytes(Stream p_Stream, byte[] p_Bytes)
        {
            p_Stream.Write(p_Bytes, 0, p_Bytes.Length);
        }

        #endregion

        #region Read Function

        public static Guid ReadGuid(Stream stream)
        {
            byte[] bytesArray = ReadByteArray(stream);
            var val = new Guid(bytesArray);
            return val;
        }

        public static Decimal ReadDecimal(Stream stream)
        {
            int v1 = ReadInt32(stream);
            int v2 = ReadInt32(stream);
            int v3 = ReadInt32(stream);
            int v4 = ReadInt32(stream);
            var val = new decimal(new[] {v1, v2, v3, v4});
            return val;
        }

        public static DateTime ReadDateTime(Stream p_Stream)
        {
            double dbl = ReadDouble(p_Stream);
            DateTime val = DateTime.FromOADate(dbl);
            return val;
        }

        public static Single ReadSingle(Stream p_Stream)
        {
            byte[] l_tmp = BitConverter.GetBytes(0.0f);
            ReadBytes(p_Stream, l_tmp);
            float val = BitConverter.ToSingle(l_tmp, 0);
            return val;
        }

        public static Double ReadDouble(Stream p_Stream)
        {
            byte[] l_tmp = BitConverter.GetBytes((Double) 0.0f);
            ReadBytes(p_Stream, l_tmp);
            double val = BitConverter.ToDouble(l_tmp, 0);
            return val;
        }

        public static Int16 ReadInt16(Stream p_Stream)
        {
            byte[] l_tmp = BitConverter.GetBytes((Int16) 0);
            ReadBytes(p_Stream, l_tmp);
            short val = BitConverter.ToInt16(l_tmp, 0);
            return val;
        }

        public static Int32 ReadInt32(Stream p_Stream)
        {
            byte[] l_tmp = BitConverter.GetBytes(0);
            ReadBytes(p_Stream, l_tmp);
            int val = BitConverter.ToInt32(l_tmp, 0);
            return val;
        }

        public static Int64 ReadInt64(Stream p_Stream)
        {
            byte[] l_tmp = BitConverter.GetBytes((Int64) 0);
            ReadBytes(p_Stream, l_tmp);
            long val = BitConverter.ToInt64(l_tmp, 0);
            return val;
        }

        //[CLSCompliant(false)]
        public static UInt16 ReadUInt16(Stream p_Stream)
        {
            byte[] l_tmp = BitConverter.GetBytes((UInt16) 0);
            ReadBytes(p_Stream, l_tmp);
            ushort val = BitConverter.ToUInt16(l_tmp, 0);
            return val;
        }

        //[CLSCompliant(false)]
        public static UInt32 ReadUInt32(Stream p_Stream)
        {
            byte[] l_tmp = BitConverter.GetBytes((UInt32) 0);
            ReadBytes(p_Stream, l_tmp);
            uint val = BitConverter.ToUInt32(l_tmp, 0);
            return val;
        }

        //[CLSCompliant(false)]
        public static UInt64 ReadUInt64(Stream p_Stream)
        {
            byte[] l_tmp = BitConverter.GetBytes((UInt64) 0);
            ReadBytes(p_Stream, l_tmp);
            ulong val = BitConverter.ToUInt64(l_tmp, 0);
            return val;
        }

        public static Byte ReadByte(Stream p_Stream)
        {
            int byteVal = p_Stream.ReadByte();
            if (byteVal == -1)
                throw new InvalidDataException();

            return (Byte) byteVal;
        }

        public static char ReadChar(Stream p_Stream)
        {
            byte[] l_tmp = BitConverter.GetBytes((char) 0);
            ReadBytes(p_Stream, l_tmp);
            char val = BitConverter.ToChar(l_tmp, 0);
            return val;
        }

        public static bool ReadBoolean(Stream p_Stream)
        {
            byte[] l_tmp = BitConverter.GetBytes(false);
            ReadBytes(p_Stream, l_tmp);
            bool val = BitConverter.ToBoolean(l_tmp, 0);
            return val;
        }

        public static string ReadString(Stream p_Stream, Encoding encoding)
        {
            byte[] bytesString = ReadByteArray(p_Stream);
            string val = encoding.GetString(bytesString);
            return val;
        }

        public static byte[] ReadByteArray(Stream p_Stream)
        {
            int len = ReadInt32(p_Stream);
            var val = new byte[len];
            ReadBytes(p_Stream, val);
            return val;
        }

        public static object Read(Stream stream, Type valueType)
        {
            if (valueType == typeof (String))
                return ReadString(stream, Encoding.UTF8);
            if (valueType == typeof (Char))
                return ReadChar(stream);
            if (valueType == typeof (Boolean))
                return ReadBoolean(stream);
            if (valueType == typeof (Decimal))
                return ReadDecimal(stream);
            if (valueType == typeof (Int16))
                return ReadInt16(stream);
            if (valueType == typeof (Int32))
                return ReadInt32(stream);
            if (valueType == typeof (Int64))
                return ReadInt64(stream);
            if (valueType == typeof (Single))
                return ReadSingle(stream);
            if (valueType == typeof (Double))
                return ReadDouble(stream);
            if (valueType == typeof (Byte))
                return ReadByte(stream);
            if (valueType == typeof (Byte[]))
                return ReadByteArray(stream);
            if (valueType == typeof (DateTime))
                return ReadDateTime(stream);
            if (valueType == typeof (Guid))
                return ReadGuid(stream);
            throw new TypeNotSupportedException(valueType);
        }

        public static void ReadBytes(Stream p_Stream, byte[] p_Value)
        {
            if (p_Stream.Read(p_Value, 0, p_Value.Length) != p_Value.Length)
                throw new InvalidDataException();
        }

        #endregion
    }


    [Serializable]
    public class InvalidDataException : DevAgeApplicationException
    {
        public InvalidDataException() :
            base("Invalid data exception")
        {
        }

        public InvalidDataException(string p_strErrDescription) :
            base(p_strErrDescription)
        {
        }

        public InvalidDataException(string p_strErrDescription, Exception p_InnerException) :
            base(p_strErrDescription, p_InnerException)
        {
        }

        protected InvalidDataException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
    }

    [Serializable]
    public class TypeNotSupportedException : DevAgeApplicationException
    {
        public TypeNotSupportedException(Type pType) :
            base("Type not supported: " + pType)
        {
        }

        public TypeNotSupportedException(string p_strErrDescription) :
            base(p_strErrDescription)
        {
        }

        public TypeNotSupportedException(string p_strErrDescription, Exception p_InnerException) :
            base(p_strErrDescription, p_InnerException)
        {
        }

        protected TypeNotSupportedException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
    }
}