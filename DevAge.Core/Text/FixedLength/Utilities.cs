#region

using System;
using System.ComponentModel;
using System.Reflection;
using DevAge.ComponentModel.Converter;
using DevAge.ComponentModel.Validator;

#endregion

namespace DevAge.Text.FixedLength
{
    ///<summary>
    ///</summary>
    public class Utilities
    {
        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        ///<param name="parseAttributes"></param>
        ///<returns></returns>
        public static IValidator CreateValidator(Type type, ParseFormatAttribute parseAttributes)
        {
            //Check for nullable
            bool nullable;
            type = GetBaseType(type, out nullable);

            TypeConverter converter = GetConverterFromPrimitiveType(type, parseAttributes);


            var Validator = new ValidatorTypeConverter(type, converter)
                                {
                                    CultureInfo = parseAttributes.CultureInfo,
                                    NullString = "",
                                    NullDisplayString = "",
                                    AllowNull = nullable
                                };

            return Validator;
        }

        private static Type GetBaseType(Type type, out bool isNullable)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                isNullable = true;
                return Nullable.GetUnderlyingType(type);
            }
            isNullable = false;
            return type;
        }

        private static TypeConverter GetConverterFromPrimitiveType(Type type, ParseFormatAttribute parseAttributes)
        {
            if (type == typeof (string))
                return new StringConverter();
            if (type == typeof (int))
                return new Int32Converter();
            if (type == typeof (double))
                return new NumberTypeConverter(typeof (double), parseAttributes.NumberFormat);
            if (type == typeof (decimal))
                return new NumberTypeConverter(typeof (decimal), parseAttributes.NumberFormat);
            if (type == typeof (DateTime))
                return new DateTimeTypeConverter(parseAttributes.DateTimeFormat, new[] {parseAttributes.DateTimeFormat});
            throw new TypeNotSupportedException(type);
        }

        ///<summary>
        ///</summary>
        ///<param name="classType"></param>
        ///<returns></returns>
        public static FieldList ExtractFieldListFromType(Type classType)
        {
            var fields = new FieldList();

            PropertyInfo[] properties =
                classType.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo prop in properties)
            {
                FieldAttribute fieldAttr = null;
                ParseFormatAttribute parseFormat = null;

                object[] attributes = prop.GetCustomAttributes(typeof (FieldAttribute), true);
                if (attributes.Length > 0)
                    fieldAttr = (FieldAttribute) attributes[0];

                attributes = prop.GetCustomAttributes(typeof (ParseFormatAttribute), true);
                if (attributes.Length > 0)
                    parseFormat = (ParseFormatAttribute) attributes[0];

                object[] valueMappings = prop.GetCustomAttributes(typeof (ValueMappingAttribute), true);

                object[] standardValues = prop.GetCustomAttributes(typeof (StandardValueAttribute), true);

                if (fieldAttr != null)
                {
                    if (parseFormat == null)
                        parseFormat = new ParseFormatAttribute();

                    IValidator validator = CreateValidator(prop.PropertyType, parseFormat);

                    var field = new Field(fieldAttr.FieldIndex, prop.Name, fieldAttr.Length, validator)
                                    {TrimBeforeParse = parseFormat.TrimBeforeParse};
                    fields.Add(field);

                    //ValueMapping - to convert specific values, can be an array of attribute, one for each conversion
                    if (valueMappings.Length > 0)
                    {
                        var mapping = new ValueMapping();
                        var valList = new object[valueMappings.Length];
                        var strList = new object[valueMappings.Length];
                        for (int i = 0; i < valueMappings.Length; i++)
                        {
                            //I convert the value assigned to the attribute to ensure that the field is valid and to support DateTime (that cannot be directly assigned to an attribute but must be declared as string)
                            valList[i] = validator.ObjectToValue(((ValueMappingAttribute) valueMappings[i]).FieldValue);

                            strList[i] = ((ValueMappingAttribute) valueMappings[i]).StringValue;
                        }

                        mapping.ThrowErrorIfNotFound = false;
                        mapping.ValueList = valList;
                        mapping.SpecialList = strList;
                        mapping.SpecialType = typeof (string);
                        mapping.BindValidator(validator);
                    }

                    //StandardValues
                    if (standardValues.Length > 0)
                    {
                        var valList = new object[standardValues.Length];
                        for (int i = 0; i < standardValues.Length; i++)
                        {
                            valList[i] = ((StandardValueAttribute) standardValues[i]).StandardValue;
                        }

                        validator.StandardValues = valList;
                        validator.StandardValuesExclusive = true;
                    }
                }
            }

            return fields;
        }

        ///<summary>
        ///</summary>
        ///<param name="separator"></param>
        ///<returns></returns>
        public static string ValidateRegExpSeparator(char separator)
        {
            if (separator == 0)
                return string.Empty;
            // See MSDN: "Character Escapes"
            // Characters other than . $ ^ { [ ( | ) * + ? \ match themselves.
            switch (separator)
            {
                case '$':
                case '^':
                case '{':
                case '[':
                case '(':
                case '|':
                case ')':
                case '*':
                case '+':
                case '?':
                case '\\':
                    return @"\" + separator;
                default:
                    return separator.ToString();
            }
        }
    }
}