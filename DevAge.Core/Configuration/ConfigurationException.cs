#region

using System;
using System.Runtime.Serialization;

#endregion

namespace DevAge.Configuration
{
    ///<summary>
    ///</summary>
    [Serializable]
    public class ConfigurationException : DevAgeApplicationException
    {
        ///<summary>
        ///</summary>
        ///<param name="p_strErrDescription"></param>
        public ConfigurationException(string p_strErrDescription) :
            base(p_strErrDescription)
        {
        }

        ///<summary>
        ///</summary>
        ///<param name="p_strErrDescription"></param>
        ///<param name="p_InnerException"></param>
        public ConfigurationException(string p_strErrDescription, Exception p_InnerException) :
            base(p_strErrDescription, p_InnerException)
        {
        }

#if !MINI
        protected ConfigurationException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
#endif
    }
}