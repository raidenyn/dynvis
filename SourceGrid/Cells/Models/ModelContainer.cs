#region

using System;
using DevAge.Collections;

#endregion

namespace SourceGrid.Cells.Models
{
    /// <summary>
    /// A container for the model classes. THe only required model is the Value model, that you can assign using the ValueModel property.
    /// </summary>
    public class ModelContainer
    {
        private ModelList m_ModelList;
        private IValueModel m_ValueModel;

        public virtual IValueModel ValueModel
        {
            get { return m_ValueModel; }
            set { m_ValueModel = value; }
        }


        /// <summary>
        /// Returns null if not exist
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public virtual IModel FindModel(Type modelType)
        {
            if (typeof (IValueModel).IsAssignableFrom(modelType))
                return m_ValueModel;
            else
            {
                if (m_ModelList == null)
                    m_ModelList = new ModelList();

                return m_ModelList.GetByType(modelType);
            }
        }


        public virtual void AddModel(IModel model)
        {
            Type type = model.GetType();

            if (typeof (IValueModel).IsAssignableFrom(type))
                m_ValueModel = (IValueModel) model;
            else
            {
                if (m_ModelList == null)
                    m_ModelList = new ModelList();

                m_ModelList.Add(model);
            }
        }

        public virtual void RemoveModel(IModel model)
        {
            if (ReferenceEquals(model, m_ValueModel))
                m_ValueModel = null;
            else if (m_ModelList != null)
                m_ModelList.Remove(model);
        }

        #region Nested type: ModelList

        /// <summary>
        /// A collection of elements of type IModel
        /// </summary>
        public class ModelList : ListByType<IModel>
        {
        }

        #endregion
    }
}