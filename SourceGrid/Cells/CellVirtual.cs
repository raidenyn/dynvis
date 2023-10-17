#region

using System;
using SourceGrid.Cells.Controllers;
using SourceGrid.Cells.Editors;
using SourceGrid.Cells.Models;
using SourceGrid.Cells.Views;

#endregion

namespace SourceGrid.Cells.Virtual
{
    /// <summary>
    /// Represents a CellVirtual in a grid.
    /// </summary>
    public class CellVirtual : ICellVirtual
    {
        #region Constructor

        /// <summary>
        /// Constructor. Create a CellVirtual using a default NullValueModel. You must provide your custom ValueModel to bind the cell to a value.
        /// </summary>
        public CellVirtual()
        {
            View = Views.Cell.Default;
            Editor = null;
            Model = new ModelContainer();
            Model.AddModel(NullValueModel.Default);
        }

        /// <summary>
        /// Constructor. Create a CellVirtual using a default NullValueModel. You must provide your custom ValueModel to bind the cell to a value.
        /// </summary>
        public CellVirtual(Type type) : this()
        {
            Editor = Factory.Create(type);
        }

        #endregion

        #region Model

        private ModelContainer m_Model;

        /// <summary>
        /// Represents the model of the cell.
        /// </summary>
        public ModelContainer Model
        {
            get { return m_Model; }
            set
            {
                if (value == null)
                    throw new SourceGridException("Model cannot be null");
                m_Model = value;
            }
        }

        #endregion

        #region View

        private IView m_View;

        /// <summary>
        /// Visual properties of this cell and other cell. You can share the VisualProperties between many cell to optimize memory size.
        /// Warning Changing this property can affect many cells
        /// </summary>
        public virtual IView View
        {
            get { return m_View; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("View");

                m_View = value;
            }
        }

        #endregion

        #region Controller

        private ControllerContainer m_Controller;

        /// <summary>
        /// Controller of the cell.
        /// </summary>
        public ControllerContainer Controller
        {
            get { return m_Controller; }
        }

        /// <summary>
        /// Add the specified controller
        /// </summary>
        /// <param name="controller"></param>
        public void AddController(IController controller)
        {
            if (m_Controller == null)
                m_Controller = new ControllerContainer();
            m_Controller.AddController(controller);
        }

        /// <summary>
        /// Remove the specifed controller
        /// </summary>
        /// <param name="controller"></param>
        public void RemoveController(IController controller)
        {
            if (m_Controller != null)
                m_Controller.RemoveController(controller);
        }

        /// <summary>
        /// Find the specified controller. Returns null if not found.
        /// </summary>
        /// <param name="pControllerType"></param>
        /// <returns></returns>
        public IController FindController(Type pControllerType)
        {
            if (m_Controller != null)
                return m_Controller.FindController(pControllerType);
            else
                return null;
        }

        #endregion

        #region Editor

        /// <summary>
        /// Editor of this cell and others cells. If null no edit is supported. 
        ///  You can share the same model between many cells to optimize memory size. Warning Changing this property can affect many cells
        /// </summary>
        public EditorBase Editor { get; set; }

        #endregion

        #region Copy

        /// <summary>
        /// Create a shallow copy of the current object. Note that this is not a deep clone, all the reference are the same.
        /// Use internally MemberwiseClone method.
        /// </summary>
        /// <returns></returns>
        public ICellVirtual Copy()
        {
            return (ICellVirtual) MemberwiseClone();
        }

        #endregion
    }
}