using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace DynVis.Core.PropertyEditor
{
    public class ObjectPropertiesEditorBase : UserControl, IPropertyEditorControl
    {
        public Control Control
        {
            get { return this; }
        }

        public void SaveChanges()
        {
            if (AcceptChanges != null) AcceptChanges(this, new EventArgs());
            if (UpdateViewObject != null) UpdateViewObject();
        }

        public void DiscartChanges()
        {
            if (RollbackChanges != null) RollbackChanges(this, new EventArgs());
            RestoreValues(EditingObject);
        }

        private readonly Dictionary<PropertyDescriptor, object> BackUpValuesList = new Dictionary<PropertyDescriptor, object>();

        private object _EditingObject;
        public object EditingObject
        {
            get
            {
                return _EditingObject;
            }
            set
            {
                _EditingObject = value;
                BackupValues(EditingObject);
                if (EditingObjectChanged != null) EditingObjectChanged(this, new EventArgs());
            }
        }

        protected void BackupValues(object obj)
        {
            var propertyCollection = TypeDescriptor.GetProperties(obj, new Attribute[] {new TransactingEditable()});
            BackUpValuesList.Clear();
            foreach (PropertyDescriptor property in propertyCollection)
            {
                var propertyType = property.PropertyType;

                var value = property.GetValue(EditingObject);

                if (value != null && propertyType.GetInterface("ICloneable") != null)
                {
                    value = ((ICloneable) value).Clone();
                }

                BackUpValuesList.Add(property, value);
            }
        }

        protected void RestoreValues(object obj)
        {
            foreach (var property in BackUpValuesList)
            {
                property.Key.SetValue(obj, property.Value);
            }
        }

        public Proc UpdateViewObject;

        public event EventHandler EditingObjectChanged;
        public event EventHandler AcceptChanges;
        public event EventHandler RollbackChanges;

        public event EventHandler EditingObjectValueChanged;

        protected void RiseEditingObjectValueChanged()
        {
            if (EditingObjectValueChanged != null) EditingObjectValueChanged(this, new EventArgs());
        }
    }
}
