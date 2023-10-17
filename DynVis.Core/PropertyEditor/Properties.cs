using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Linq;
using DynVis.EventsLog;

namespace DynVis.Core.PropertyEditor
{
    /// <summary>
    /// Класс для сохранения и востановления свойств произвольных объектов.
    /// Обрабатывая объект помещает значения его свойств помечанных атрибутом SavableProperty в виде строки.
    /// По закрытию приложения помещает данные в файл XML формата.
    /// При первом обращении к себе считывает свойства из файла в формате XML с последующей возможностью
    /// заполнить свойства любого объекта известного типа.  
    /// </summary>
    internal static class Properties
    {
        private static Dictionary<string, Dictionary<string, string>> PropertiesCollection;

        /// <summary>
        /// Название файла параметров
        /// </summary>
        private const string ParamsFileName = "properties.inf";

        /// <summary>
        /// Корневой элемент XML
        /// </summary>
        private const string XmlRootElementName = "DynVisProerty";

        static Properties()
        {
            OpenFile();

            Application.ApplicationExit += Application_ApplicationExit;
        }

        /// <summary>
        /// Сохраняем файл по закрытию приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            SaveFile();
        }

        /// <summary>
        /// Открывает файл и читает из него данные
        /// </summary>
        private static void OpenFile()
        {
            var result = false;
            try
            {
                var XmlDocument = new XmlDocument();
                XmlDocument.Load(ParamsFileName);
                PropertiesCollection = GetFromXml(XmlDocument);
                result = true;
            }
            catch (IOException e)
            {
                PropertiesCollection = new Dictionary<string, Dictionary<string, string>>();
                Log.LogError(e.ToString());
            }
            catch (XmlException e)
            {
                PropertiesCollection = new Dictionary<string, Dictionary<string, string>>();
                Log.LogError(e.ToString());
            }
            Log.LogResult("Property file loaded", result);
        }

        /// <summary>
        /// Сохраняет файл с данными
        /// </summary>
        private static void SaveFile()
        {
            try
            {
                GetAsXml().Save(ParamsFileName);
            }
            catch (IOException) { }
        }

        /// <summary>
        /// Ищет список свйоств для задонного типа объекта
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetTypePropertiesList(this Type type)
        {
            var r = from t in PropertiesCollection where type.FullName.CompareTo(t.Key) == 0 select t.Value;

            var result = r.FirstOrDefault();

            if (result == null)
            {
                result = new Dictionary<string, string>();
                PropertiesCollection.Add(type.FullName, result);
            }
            return result;
        }

        /// <summary>
        /// Возращает значения свойства объекта.
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="name">Название свойства</param>
        /// <param name="defaultValue">Значение по умолчанию (Возращается при отсутсвии сохраненного значения)</param>
        /// <param name="valueType">Тип значения</param>
        /// <returns></returns>
        private static object GetPropertyValue(this object obj, string name, object defaultValue, Type valueType)
        {
            if (obj != null)
            {
                var propertiesList = GetTypePropertiesList(obj.GetType());
                string strValue;
                if (propertiesList.TryGetValue(name, out strValue))
                {
                    var value = ConverterObject.ConvertFromString(strValue, valueType);
                    if (value != null)
                    {
                        return value;
                    }
                }
            }
            obj.SetPropertyValue(name, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Сохраняет значение свойства объекта
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="name">Название свойства</param>
        /// <param name="value">Значение</param>
        private static void SetPropertyValue(this object obj, string name, object value)
        {
            if (obj != null)
            {
                var propertiesList = GetTypePropertiesList(obj.GetType());
                var stringValue = ConverterObject.ConvertToString(value);
                if (propertiesList.ContainsKey(name))
                {
                    propertiesList[name] = stringValue;
                } 
                else
                {
                    propertiesList.Add(name, stringValue);
                }
            }
        }

        /// <summary>
        /// Флаг управляющий глубинной рефлексии объекта.
        /// </summary>
        private const BindingFlags BindingFlag = (BindingFlags.Public | BindingFlags.Instance);

        /// <summary>
        /// Сохраняет все свойства объекта помечанные как SavableProperty
        /// </summary>
        /// <param name="obj"></param>
        public static void SaveAllValues(object obj)
        {
            if (obj != null)
            {
                var type = obj.GetType();

                var properties = type.GetProperties(BindingFlag);

                foreach (var property in properties)
                {
                    if (property.GetCustomAttributes(typeof(SavableProperty), false).Length > 0)
                    {
                        obj.SetPropertyValue(property.Name, property.GetValue(obj, null));
                    }
                }
            }
        }

        /// <summary>
        /// Востанавливает все свойста объекта помечанные как SavableProperty
        /// </summary>
        /// <param name="obj"></param>
        public static void LoadAllValues(object obj)
        {
            if (obj != null)
            {
                var type = obj.GetType();

                var properties = type.GetProperties(BindingFlag);

                foreach (var property in properties)
                {
                    if (property.GetCustomAttributes(typeof(SavableProperty), false).Length > 0)
                    {
                        property.SetValue(obj, obj.GetPropertyValue(property.Name, property.GetValue(obj, null), property.PropertyType), null);
                    }
                }

            }
        }

        /// <summary>
        /// Конвертирует данные в XML
        /// </summary>
        /// <returns></returns>
        private static XmlDocument GetAsXml()
        {
            var xmlDocument = new XmlDocument();
            var rootNode = xmlDocument.CreateElement(XmlRootElementName);

            foreach (var type in PropertiesCollection)
            {
                var typeNode = rootNode.OwnerDocument.CreateElement(type.Key);

                SetProperties(typeNode, type.Value);

                rootNode.AppendChild(typeNode);
            }

            xmlDocument.AppendChild(rootNode);

            return xmlDocument;
        }

        private static void SetProperties(XmlNode node, Dictionary<string, string> properties)
        {
            foreach (var property in properties)
            {
                if (!String.IsNullOrEmpty(property.Value))
                {
                    var propertyNode = node.OwnerDocument.CreateElement(property.Key);

                    propertyNode.InnerXml = property.Value;

                    node.AppendChild(propertyNode);
                }
            }
        }

        /// <summary>
        /// Конвертирует XML в список.
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        private static Dictionary<string, Dictionary<string, string>> GetFromXml(XmlNode xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;

            var propertiesCollection = new Dictionary<string, Dictionary<string, string>>();

            if (rootNode.Name == XmlRootElementName)
            {
                foreach (XmlNode typeNode in rootNode.ChildNodes)
                {
                    var PropertyList = new Dictionary<string, string>();

                    foreach (XmlNode propertyNode in typeNode.ChildNodes)
                    {
                        PropertyList.Add(propertyNode.Name, propertyNode.InnerXml);
                    }

                    propertiesCollection.Add(typeNode.Name, PropertyList);
                }

            }

            return propertiesCollection;
        }
    }

    /// <summary>
    /// Этим атрибутом помечаются свойства которые должны быть сохранены
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SavableProperty: Attribute
    {
        
    }

    /// <summary>
    /// Класс для конвертацнии объектов в строку и обратно.
    /// Поддерживается работа с:
    ///     Все встроеные типы включая enum
    ///     Все объекты поддерживающие IConvertible в строку и обратно
    ///     Color (строка содержит значение IntARGB)
    ///     Pen (не полностью)
    ///     Font (не полностью)
    ///     Brush как SolidBrush
    /// </summary>
    internal static class ConverterObject
    {
        static ConverterObject()
        {
            Culture.SetPointAsDecimalSeparator();
        }
        
        /// <summary>
        /// Перобразования объекта в строку
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertToString(object obj)
        {
            Type type = GetType(obj);

            if (type != null)
            {
                if (type.GetInterface("IConvertible") != null)
                {
                    return (string)Convert.ChangeType(obj, typeof(string));
                }
                if (type == typeof(Color))
                {
                    return ColorToString((Color)obj);
                }
                if (type == typeof(Font))
                {
                    return FontToString((Font)obj);
                }
                if (type == typeof(Pen))
                {
                    return PenToString((Pen)obj);
                }
                if (type == typeof(SolidBrush))
                {
                    return BrushToString((SolidBrush)obj);
                }
            } 


            return String.Empty;
        }

        /// <summary>
        /// Преобразование строки в объект
        /// </summary>
        /// <param name="str"></param>
        /// <param name="resultType">Тип объекта</param>
        /// <returns></returns>
        public static object ConvertFromString(string str, Type resultType)
        {
            if (resultType.GetInterface("IConvertible") != null)
            {
                return ConvertType(str, resultType);
            }
            if (resultType == typeof(Color))
            {
                return StringToColor(str);
            }
            if (resultType == typeof(Font))
            {
                return StringToFont(str);
            }
            if (resultType == typeof(Brush))
            {
                return StringToBrush(str);
            }
            if (resultType == typeof(Pen))
            {
                return StringToPen(str);
            }

            return null;
        }

        /// <summary>
        /// Получает тип объекта. Если объект равен null, возращает null. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Type GetType(object obj)
        {
            return obj != null ? obj.GetType() : null;
        }

        /// <summary>
        /// Преобразует баззовые типы включая enum
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object ConvertType(string obj, Type type)
        {
            return type.IsEnum ? Enum.Parse(type, obj, true) : Convert.ChangeType(obj, type);
        }

        #region Color
        private static string ColorToString(Color color)
        {
            return color.ToArgb().ToString();
        }
        private static Color StringToColor(string str)
        {
            try
            {
                return Color.FromArgb(int.Parse(str));
            } catch (InvalidCastException)
            {
                return Color.White;
            }

        }
        #endregion

        #region Font
        class SerializableFont
        {
            // ReSharper disable UnusedMemberInPrivateClass
            public SerializableFont()
                // ReSharper restore UnusedMemberInPrivateClass
                : this(new Font("Courier Now", 10))
            {

            }

            public SerializableFont(Font font)
            {
                Font = font;
            }

            public Font Font;

            [Savable]
            public string Name
            {
                get { return Font.Name; }
                set { Font = new Font(value, Font.Size); }
            }
            [Savable]
            public float Size
            {
                get { return Font.Size; }
                set { Font = new Font(Font.Name, value); }
            }
            [Savable]
            public FontStyle Style
            {
                get { return Font.Style; }
                set { Font = new Font(Font.Name, Font.Size, value); }
            }
        }

        private static string FontToString(Font font)
        {
            return Serializer.Serialize(new SerializableFont(font));
        }

        private static Font StringToFont(string str)
        {
            try
            {
                return Serializer.Deserialize<SerializableFont>(str).Font;
            }
            catch (XmlException) {
                return null; }
        }
        #endregion

        #region Brush
        private static string BrushToString(Brush brush)
        {
            var solidBrush = brush as SolidBrush;
            if (solidBrush != null)
            {
                return ColorToString(solidBrush.Color);
            }
            return null;
        }

        private static Brush StringToBrush(string str)
        {
            return new SolidBrush(StringToColor(str));
        }
        #endregion

        #region Pen
        class SerializablePen
        {
            // ReSharper disable UnusedMemberInPrivateClass
            public SerializablePen()
                // ReSharper restore UnusedMemberInPrivateClass
                : this(new Pen(System.Drawing.Color.White))
            {

            }

            public SerializablePen(Pen pen)
            {
                Pen = pen;
            }

            public readonly Pen Pen;

            [Savable]
            public string Color
            {
                get { return ColorToString(Pen.Color); }
                set { Pen.Color = StringToColor(value); }
            }
            [Savable]
            public float Width
            {
                get { return Pen.Width; }
                set { Pen.Width = value; }
            }
            [Savable]
            public DashStyle DashStyle
            {
                get { return Pen.DashStyle; }
                set { Pen.DashStyle = value; }
            }
        }

        private static string PenToString(Pen pen)
        {
            return Serializer.Serialize(new SerializablePen(pen));
        }

        private static Pen StringToPen(string str)
        {
            try
            {
                return Serializer.Deserialize<SerializablePen>(str).Pen;
            } catch (XmlException)
            {
                return null;
            }
        }

        #endregion
    }
}
