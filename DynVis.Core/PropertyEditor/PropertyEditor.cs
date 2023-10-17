using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DynVis.Core.PropertyEditor
{
    /// <summary>
    /// Интерфейс предоставляющего информацию о редактировании свойств объекта
    /// </summary>
    public interface IPropertiesObject
    {
        /// <summary>
        /// Редактируемый объект
        /// </summary>
        object EditableObject { get; }
        
        /// <summary>
        /// Название объекта
        /// </summary>
        string PropertiesObjectName { get; }

        /// <summary>
        /// Название секции редактирования
        /// </summary>
        string SectionName { get; }

        /// <summary>
        /// Функция возращает инрефейс редактора объекта
        /// </summary>
        /// <returns></returns>
        IPropertyEditorControl GetPropertiesEditor();
    }

    /// <summary>
    /// Интерфейс редактора объекта
    /// </summary>
    public interface IPropertyEditorControl
    {
        /// <summary>
        /// Контрол редактора
        /// </summary>
        Control Control { get; }

        /// <summary>
        /// Функция сохранения значения
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Функция отката всех изменений с последнего сохранения
        /// </summary>
        void DiscartChanges();

        /// <summary>
        /// Событие возникающее при любом изменении объекта
        /// </summary>
        event EventHandler EditingObjectValueChanged;
    }

    /// <summary>
    /// Объект работы с со свойствами на уровне всего приложения
    /// Позволяет добовлять объекты в список настраиваемых и отображать в форме настройки свойств
    /// </summary>
    public static class GlobalProperiesEditor
    {
        /// <summary>
        /// Список зарегестрированных объектов
        /// </summary>
        private readonly static List<IPropertiesObject> EditableObjects = new List<IPropertiesObject>();
        
        /// <summary>
        /// Регестрация объекта редактирования
        /// </summary>
        /// <param name="obj"></param>
        public static void RegisterEditableObject(IPropertiesObject obj)
        {
            if (!EditableObjects.Contains(obj))
            {
                Properties.LoadAllValues(obj.EditableObject);
                EditableObjects.Add(obj);
            }
        }

        /// <summary>
        /// Удаления регистрации
        /// </summary>
        /// <param name="obj"></param>
        public static void UnregisterEditableObject(IPropertiesObject obj)
        {
            EditableObjects.Remove(obj);
            Properties.SaveAllValues(obj.EditableObject);
        }

        /// <summary>
        /// Функция копирует элементы списка из одного в другой недопуская повторений
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        private static void CopyUniqueItems<T>(IEnumerable<T> source, ICollection<T> destination)
        {
            foreach (var item in source)
            {
                if (!destination.Contains(item)) destination.Add(item);
            }
        }

        /// <summary>
        /// Сохраняет значения всех объектов
        /// </summary>
        public static void SaveAllProperites()
        {
            foreach (var item in EditableObjects)
            {
                Properties.SaveAllValues(item.EditableObject);
            }
        }

        /// <summary>
        /// Показывает форму настройки как диалог
        /// </summary>
        /// <param name="owner">Владелец формы настройки</param>
        /// <returns>Возращаемое диалогом значение</returns>
        public static DialogResult ShowEditorDialog(IWin32Window owner)
        {
            if (EditableObjects.Count > 0)
            {
                var FormProperties = new FormProperties();
                CopyUniqueItems(EditableObjects, FormProperties.EditableObjects);
                return FormProperties.ShowDialog(owner);
            }
            return DialogResult.None;
        }

        /// <summary>
        /// Показывает форму настройки только для одного объекта как диалог 
        /// </summary>
        /// <param name="owner">Владелец формы настройки</param>
        /// <param name="obj">Редактируемый объект</param>
        /// <returns>Возращаемое диалогом значение</returns>
        public static DialogResult ShowEditorDialog(IWin32Window owner, IPropertiesObject obj)
        {
            if (EditableObjects.Count > 0)
            {
                var FormProperties = new FormProperties();
                FormProperties.EditableObjects.Add(obj);
                return FormProperties.ShowDialog(owner);
            }
            return DialogResult.None;
        }

        /// <summary>
        /// Показывает форму настройки как отдельное окно
        /// </summary>
        /// <param name="owner">Владелец формы настройки</param>
        public static void ShowEditor(IWin32Window owner)
        {
            if (EditableObjects.Count > 0)
            {
                var FormProperties = (FormProperties)ApplicationExtension.FindOpenForm(typeof(FormProperties)) ??
                                     new FormProperties();

                CopyUniqueItems(EditableObjects, FormProperties.EditableObjects);
                if (!FormProperties.Visible) FormProperties.Show(owner);
            }
        }

        /// <summary>
        /// Показывает форму настройки только для одного объекта как отдельное окно
        /// </summary>
        /// <param name="owner">Владелец формы настройки</param>
        /// <param name="obj"></param>
        public static void ShowEditor(IWin32Window owner, IPropertiesObject obj)
        {
            if (EditableObjects.Count > 0)
            {
                var FormProperties = (FormProperties)ApplicationExtension.FindOpenForm(typeof(FormProperties)) ??
                                     new FormProperties();

                if (!FormProperties.EditableObjects.Contains(obj))
                {
                    FormProperties.EditableObjects.Add(obj);
                }

                FormProperties.SelectedObject = obj;

                if (!FormProperties.Visible) FormProperties.Show(owner);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class TransactingEditable: Attribute
    {
        
    }
}
