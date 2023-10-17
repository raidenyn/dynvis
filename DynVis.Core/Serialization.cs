using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Xml;

namespace DynVis.Core
{
    /// <summary>
    /// Клас реализующий различные функции посредствам сериализации
    /// </summary>
    public static class Serialization
    {
        public static T Clone<T>(T obj)
        {
            var stream = new MemoryStream();
            var Serialiser = new BinaryFormatter();
            Serialiser.Serialize(stream, obj);
            stream.Position = 0;
 
            return (T)Serialiser.Deserialize(stream);
        }

        private const BindingFlags BindingFlag = (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        private static void GetAllSerializableTypes(Type type, ICollection<Type> listType)
        {
            while (type != typeof(object))
            {
                var fields = type.GetFields(BindingFlag);

                foreach (var field in fields)
                {
                    var fieldType = field.FieldType;
                    if (fieldType.IsClass)
                    {
                        if (!listType.Contains(fieldType))
                        {
                            listType.Add(fieldType);
                            GetAllSerializableTypes(fieldType, listType);
                        }
                    }
                }

                type = type.BaseType;
            }
        }

        public static Type[] GetAllSerializableTypes(Type type)
        {
            var result = new List<Type>();
            GetAllSerializableTypes(type, result);
            return result.ToArray();
        }
    }

    /// <summary>
    /// Атрибут указывающий на необходимость сохранения класса или поля
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class Savable : Attribute
    {
        
    }

    /// <summary>
    /// Атрибут указывающий на запрет сохранения класса или поля
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class NotSavable : Attribute
    {

    }

    /// <summary>
    /// Сериализатор в тестовый Xml формат
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Сериализует объект в поток
        /// При сериализации операется на атрибуты Savable и  NotSavable
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="stream">Поток</param>
        public static void Serialize(object obj, Stream stream)
        {
            var XmlDocument = new XmlDocument();

            SaveAllSerializableTypes(obj, XmlDocument.AppendChild(XmlDocument.CreateElement(obj.GetType().GetSerializableName())));

            XmlDocument.Save(stream);
        }

        /// <summary>
        /// Сериализует объект в строку
        /// При сериализации операется на атрибуты Savable и  NotSavable
        /// </summary>
        /// <param name="obj">Объект</param>
        public static string Serialize(object obj)
        {
            var XmlDocument = new XmlDocument();

            SaveAllSerializableTypes(obj, XmlDocument.AppendChild(XmlDocument.CreateElement(obj.GetType().GetSerializableName())));

            return XmlDocument.OuterXml;
        }

        /// <summary>
        /// Десириализует объект из потока
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="stream">Поток</param>
        /// <returns>Объект</returns>
        public static T Deserialize<T>(Stream stream)
        {
            var xmlDocument = new XmlDocument();

            xmlDocument.Load(stream);

            return Deserialize<T>(xmlDocument);
        }

        /// <summary>
        /// Десириализует объект из строки
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="str">Строка</param>
        /// <returns>Объект</returns>
        public static T Deserialize<T>(string str)
        {
            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(str);

            return Deserialize<T>(xmlDocument);
        }

        private static T Deserialize<T>(XmlNode xmlDocument)
        {
            if (xmlDocument.HasChildNodes)
            {
                var RootElement = xmlDocument.FirstChild;

                if (RootElement.Name == typeof(T).GetSerializableName())
                {
                    var obj = (T)Activator.CreateInstance(typeof(T));

                    LoadAllSerializableTypes(obj, RootElement);

                    return obj;
                }
            }

            return default(T);
        }
        
        private static string GetSerializableName(this Type type)
        {
            return type.Name.Replace("`", "");
        }

        private const BindingFlags BindingFlag = (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        private static bool AtributePresent(this ICustomAttributeProvider type, Type atributeType)
        {
            return type.GetCustomAttributes(atributeType, false).Length > 0;
        }

        private static bool IsSavable(this ICustomAttributeProvider type)
        {
            return type.AtributePresent(typeof(Savable));
        }

        private static bool IsNotSavable(this ICustomAttributeProvider type)
        {
            return type.AtributePresent(typeof(NotSavable));
        }

        private static bool IsValidName(this string name)
        {
            return name.IndexOf(">") == -1 && name.IndexOf("<") == -1;
        }

        private static bool IsSerializableType(this Type type)
        {
            return type.IsClass && type != typeof(string);
        }

        private static void SaveMembers(IEnumerable<MemberInfo> members, ICustomAttributeProvider type, XmlNode node, Func<MemberInfo, object> getValue)
        {
            foreach (var member in members)
            {
                if (((type.IsSavable() || member.IsSavable()) && !member.IsNotSavable()) && member.Name.IsValidName())
                {
                    var currentElement = node.OwnerDocument.CreateElement(member.Name);

                    var fieldValue = getValue(member);

                    if (fieldValue != null)
                    {
                        var fieldType = fieldValue.GetType();

                        if (fieldType.IsSerializableType())
                        {
                            SaveAllSerializableTypes(fieldValue, currentElement);
                        }
                        else
                        {
                            currentElement.InnerText = fieldValue.ToString();
                        }
                    }


                    node.AppendChild(currentElement);
                }
            }
        }

        private static void SaveAllSerializableTypes(object obj, XmlNode node)
        {
            var type = obj.GetType();

            if (type.IsSerializableType())
            {
                while (type != typeof(object))
                {
                    var fields = type.GetFields(BindingFlag);

                    SaveMembers(fields, type, node, member => (member as FieldInfo).GetValue(obj));

                    var properties = type.GetProperties();

                    SaveMembers(properties, type, node, member => (member as PropertyInfo).GetValue(obj, null));

                    type = type.BaseType;
                }

                /*
                if (obj.GetType().GetInterface("IDictionary") != null)
                {
                    var currentElement = node.OwnerDocument.CreateElement("List");
                    foreach (DictionaryEntry item in (IDictionary)obj)
                    {
                        var keyNode = node.OwnerDocument.CreateElement(item.Key.ToString());
                        SaveAllSerializableTypes(item.Value, keyNode);
                        node.AppendChild(keyNode);
                    }
                    node.AppendChild(currentElement);
                }
                */
            } 
            else
            {
                var currentElement = node.OwnerDocument.CreateElement(obj.GetType().GetSerializableName());
                currentElement.InnerText = obj.ToString();
                node.AppendChild(currentElement);
            }


        }


        private static FieldInfo GetFieldByName(this IEnumerable<FieldInfo> fields, string fieldName)
        {
            var fildsEnum = from field in fields where fieldName.CompareTo(field.Name) == 0 select field;
            return fildsEnum.FirstOrDefault();
        }

        private static PropertyInfo GetPropertyByName(this IEnumerable<PropertyInfo> properties, string propertyName)
        {
            var propertiesEnum = from property in properties where propertyName.CompareTo(property.Name) == 0 select property;
            return propertiesEnum.FirstOrDefault();
        }

        private static object ConvertType(string obj, Type type)
        {
            return type.IsEnum ? Enum.Parse(type, obj, true) : Convert.ChangeType(obj, type);
        }

        private static void LoadField(FieldInfo field, XmlNode element, object obj)
        {
            var fieldType = field.FieldType;

            object fieldValue;

            if (fieldType.IsSerializableType())
            {
                fieldValue = Activator.CreateInstance(fieldType);

                LoadAllSerializableTypes(fieldValue, element);

                field.SetValue(obj, fieldValue);
            }
            else
            {
                var fieldValueString = element.InnerText;

                fieldValue = null;

                if (!String.IsNullOrEmpty(fieldValueString))
                {
                    fieldValue = ConvertType(fieldValueString, fieldType);
                }
            }

            field.SetValue(obj, fieldValue);
        }

        private static bool TryLoadField(string fieldName, IEnumerable<FieldInfo> fields, XmlNode element, object obj)
        {
            var field = fields.GetFieldByName(fieldName);

            if (field != null)
            {
                LoadField(field, element, obj);
                return true;
            }
            return false;
        }

        private static void LoadProperty(PropertyInfo property, XmlNode element, object obj)
        {
            if (property.GetSetMethod(true) != null)
            {
                var propertyType = property.PropertyType;

                object propertyValue;

                if (propertyType.IsSerializableType())
                {
                    propertyValue = Activator.CreateInstance(propertyType);

                    LoadAllSerializableTypes(propertyValue, element);
                }
                else
                {
                    var propertyValueString = element.InnerText;

                    propertyValue = null;

                    if (!String.IsNullOrEmpty(propertyValueString))
                    {
                        propertyValue = ConvertType(propertyValueString, propertyType);
                    }
                }

                property.SetValue(obj, propertyValue, null);
            }
        }

        private static void TryLoadProperty(string propertyName, IEnumerable<PropertyInfo> properties, XmlNode element, object obj)
        {
            var property = properties.GetPropertyByName(propertyName);

            if (property != null)
            {
                LoadProperty(property, element, obj);
            }
        }

        private static void LoadAllSerializableTypes(object obj, XmlNode node)
        {
            var type = obj.GetType();

            while (type != typeof(object))
            {
                var fields = type.GetFields(BindingFlag);
                var properties = type.GetProperties(BindingFlag);

                foreach (XmlNode element in node.ChildNodes)
                {
                    var memebrName = element.Name;

                    if (!TryLoadField(memebrName, fields, element, obj))
                    {
                        TryLoadProperty(memebrName, properties, element, obj);
                    }
                }

                type = type.BaseType;
            }
        }

        
    }
}
