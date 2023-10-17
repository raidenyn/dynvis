#region

using System;
using System.Globalization;
using DevAge.ComponentModel.Validator;

#endregion

namespace DevAge.Configuration
{
    ///<summary>
    ///</summary>
    public class PersistableItem
    {
        private readonly string m_Name;
        private readonly Type m_Type;
        private readonly IValidator m_Validator;
        private object m_DefaultValue;
        private object m_Value;

        ///<summary>
        ///</summary>
        ///<param name="pType"></param>
        ///<param name="pName"></param>
        ///<param name="pDefaultValue"></param>
        public PersistableItem(Type pType, string pName, object pDefaultValue)
        {
            m_Validator = new ValidatorTypeConverter(pType)
                              {
                                  CultureInfo = CultureInfo.InvariantCulture
                              };
            //N.B. I have used invariant culture to ensure to correct parsing and transformation with a standard (english) format

            m_Type = pType;
            m_Name = pName;
            m_Value = pDefaultValue;
            m_DefaultValue = pDefaultValue;
        }

        ///<summary>
        ///</summary>
        public IValidator Validator
        {
            get { return m_Validator; }
        }

        ///<summary>
        ///</summary>
        public Type Type
        {
            get { return m_Type; }
        }

        ///<summary>
        ///</summary>
        public string Name
        {
            get { return m_Name; }
        }

        ///<summary>
        ///</summary>
        public object Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        ///<summary>
        ///</summary>
        public object DefaultValue
        {
            get { return m_DefaultValue; }
            set { m_DefaultValue = value; }
        }

        ///<summary>
        ///</summary>
        public bool IsChanged
        {
            get
            {
                if (m_Value == null && m_DefaultValue == null)
                    return false;
                if (m_Value == null)
                    return true;

                return !(m_Value.Equals(m_DefaultValue));
            }
        }

        ///<summary>
        ///</summary>
        public void AcceptAsDefault()
        {
            m_DefaultValue = m_Value;
        }

        ///<summary>
        ///</summary>
        public void Reset()
        {
            m_Value = m_DefaultValue;
        }

        public override string ToString()
        {
            return "PersistableItem: " + Name + "=" + Validator.ValueToDisplayString(Value);
        }
    }
}