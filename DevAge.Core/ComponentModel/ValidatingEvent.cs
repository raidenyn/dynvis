#region

using System;

#endregion

namespace DevAge.ComponentModel
{
    ///<summary>
    ///</summary>
    public class ConvertingObjectEventArgs : EventArgs
    {
        private readonly Type m_DestinationType;
        private ConvertingStatus m_ConvertingStatus = ConvertingStatus.Converting;

        ///<summary>
        ///</summary>
        ///<param name="p_Value"></param>
        ///<param name="p_DestinationType"></param>
        public ConvertingObjectEventArgs(object p_Value, Type p_DestinationType)
        {
            Value = p_Value;
            m_DestinationType = p_DestinationType;
        }

        ///<summary>
        ///</summary>
        public object Value { get; set; }

        /// <summary>
        /// Destination type to convert the Value. Can be null if no destination type is required.
        /// </summary>
        public Type DestinationType
        {
            get { return m_DestinationType; }
        }

        ///<summary>
        ///</summary>
        public ConvertingStatus ConvertingStatus
        {
            get { return m_ConvertingStatus; }
            set { m_ConvertingStatus = value; }
        }
    }

    ///<summary>
    ///</summary>
    ///<param name="sender"></param>
    ///<param name="e"></param>
    public delegate void ConvertingObjectEventHandler(object sender, ConvertingObjectEventArgs e);

    ///<summary>
    ///</summary>
    public enum ConvertingStatus
    {
        ///<summary>
        ///</summary>
        Converting = 0,
        ///<summary>
        ///</summary>
        Error = 1,
        ///<summary>
        ///</summary>
        Completed = 2
    }

    ///<summary>
    ///</summary>
    public class ValueEventArgs : EventArgs
    {
        ///<summary>
        ///</summary>
        ///<param name="p_Value"></param>
        public ValueEventArgs(object p_Value)
        {
            Value = p_Value;
        }

        ///<summary>
        ///</summary>
        public object Value { get; set; }
    }

    ///<summary>
    ///</summary>
    ///<param name="sender"></param>
    ///<param name="e"></param>
    public delegate void ValueEventHandler(object sender, ValueEventArgs e);
}