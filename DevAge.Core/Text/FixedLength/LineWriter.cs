#region

using System;
using System.Reflection;
using System.Text;

#endregion

namespace DevAge.Text.FixedLength
{
    /// <summary>
    /// A class used to create fixed lenght string.
    /// </summary>
    public class LineWriter
    {
        private readonly FieldList mFields;
        private object[] mLineValues;
        private char mSeparator = '\0';
        private IField[] mSortedList;

        public LineWriter(FieldList fields)
        {
            mFields = fields;
        }

        public LineWriter(Type lineClassType)
        {
            mFields = Utilities.ExtractFieldListFromType(lineClassType);
        }

        public FieldList Fields
        {
            get { return mFields; }
        }

        public char Separator
        {
            get { return mSeparator; }
            set { mSeparator = value; }
        }

        public void Reset()
        {
            mLineValues = null;
        }

        public void SetValue(string fieldName, object val)
        {
            if (mLineValues == null)
                mLineValues = new object[mFields.Count];
            if (mSortedList == null)
                mSortedList = mFields.GetSortedList();

            mLineValues[mFields[fieldName].Index] = val;
        }

        public string CreateLine()
        {
            if (mLineValues == null)
                throw new ArgumentNullException("mLineValues", "SetValue not called");
            if (mSortedList == null)
                mSortedList = mFields.GetSortedList();

            var builder = new StringBuilder();
            for (int i = 0; i < mSortedList.Length; i++)
            {
                builder.Append(mSortedList[i].ValueToString(mLineValues[i]));

                if (Separator != '\0')
                    builder.Append(Separator);
            }

            return builder.ToString();
        }

        public string CreateLineFromClass(object schemaClass)
        {
            PropertyInfo[] properties =
                schemaClass.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance |
                                                    BindingFlags.Public);

            foreach (PropertyInfo prop in properties)
            {
                object[] attributes = prop.GetCustomAttributes(typeof (FieldAttribute), true);
                if (attributes.Length > 0)
                {
                    //FieldAttribute fieldAttr = (FieldAttribute)attributes[0];
                    SetValue(prop.Name, prop.GetValue(schemaClass, null));
                }
            }

            return CreateLine();
        }
    }
}