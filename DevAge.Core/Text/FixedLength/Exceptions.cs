#region

using System;
using System.Runtime.Serialization;

#endregion

namespace DevAge.Text.FixedLength
{
    ///<summary>
    ///</summary>
    [Serializable]
    public class InvalidFieldLengthException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="length"></param>
        public InvalidFieldLengthException(int length) :
            base("Invalid field length " + length + " must be a positive number.")
        {
        }

#if !MINI
        protected InvalidFieldLengthException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif
    }

    ///<summary>
    ///</summary>
    [Serializable]
    public class ValueNotValidLengthException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="value"></param>
        ///<param name="expectedLength"></param>
        public ValueNotValidLengthException(string value, int expectedLength) :
            base("Value " + value + " not valid, length must be " + expectedLength)
        {
        }

#if !MINI
        protected ValueNotValidLengthException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif
    }

    ///<summary>
    ///</summary>
    [Serializable]
    public class ValueNotSupportedException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="value"></param>
        ///<param name="type"></param>
        public ValueNotSupportedException(string value, Type type) :
            base("Value " + value + " not supported, type is " + type.Name)
        {
        }

#if !MINI
        protected ValueNotSupportedException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif
    }

    ///<summary>
    ///</summary>
    [Serializable]
    public class TypeNotSupportedException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        public TypeNotSupportedException(Type type) :
            base("Type " + type + " not supported")
        {
        }

#if !MINI
        protected TypeNotSupportedException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif
    }

    ///<summary>
    ///</summary>
    [Serializable]
    public class RegExException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="group"></param>
        public RegExException(string group) :
            base("Regular expression group " + group + " not valid")
        {
        }

#if !MINI
        protected RegExException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif
    }

    ///<summary>
    ///</summary>
    [Serializable]
    public class FieldParseException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="name"></param>
        ///<param name="valToParse"></param>
        ///<param name="innerException"></param>
        public FieldParseException(string name, string valToParse, Exception innerException) :
            base("Failed to parse field " + name + " '" + valToParse + "' - " + innerException.Message, innerException)
        {
        }

#if !MINI
        protected FieldParseException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif
    }

    ///<summary>
    ///</summary>
    [Serializable]
    public class FieldStringConvertException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="name"></param>
        ///<param name="value"></param>
        ///<param name="innerException"></param>
        public FieldStringConvertException(string name, object value, Exception innerException) :
            base(
            "Failed to convert to string field " + name + " '" + ObjectToStringForError(value) + "' - " +
            innerException.Message, innerException)
        {
        }

#if !MINI
        protected FieldStringConvertException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif

        /// <summary>
        /// Returns a string used for error description for a specified object. Usually used when printing the object for the error message when there is a conversion error.
        /// </summary>
        /// <param name="val"></param>
        private static string ObjectToStringForError(object val)
        {
            try
            {
                if (val == null)
                    return "<null>";
                return val.ToString();
            }
            catch (Exception)
            {
                return "<object>";
            }
        }
    }


    ///<summary>
    ///</summary>
    [Serializable]
    public class FieldNotDefinedException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="fieldIndex"></param>
        public FieldNotDefinedException(int fieldIndex) :
            base("Field " + fieldIndex + " not defined.")
        {
        }

#if !MINI
        protected FieldNotDefinedException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif
    }

    ///<summary>
    ///</summary>
    [Serializable]
    public class FailedPropertySetFieldException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="field"></param>
        ///<param name="innerException"></param>
        public FailedPropertySetFieldException(string field, Exception innerException) :
            base("Failed to set property for field " + field + " - " + innerException.Message, innerException)
        {
        }

#if !MINI
        protected FailedPropertySetFieldException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif
    }

    ///<summary>
    ///</summary>
    [Serializable]
    public class FailedPropertyGetFieldException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="field"></param>
        ///<param name="innerException"></param>
        public FailedPropertyGetFieldException(string field, Exception innerException) :
            base("Failed to get property for field " + field + " - " + innerException.Message, innerException)
        {
        }

#if !MINI
        protected FailedPropertyGetFieldException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif
    }
}