#region

using System;
using SourceGrid.Cells.Controllers;
using SourceGrid.Cells.Editors;
using SourceGrid.Cells.Models;
using SourceGrid.Cells.Views;

#endregion

namespace SourceGrid.Cells
{
    /// <summary>
    /// Interface to represents a cell virtual (without position or value information).
    /// </summary>
    public interface ICellVirtual
    {
        #region Editor

        /// <summary>
        /// Editor of this cell and others cells. If null no edit is supported. 
        ///  You can share the same model between many cells to optimize memory size. Warning Changing this property can affect many cells
        /// </summary>
        EditorBase Editor { get; set; }

        #endregion

        #region Controller

        /// <summary>
        /// Controller of the cell. Represents the actions of a cell.
        /// </summary>
        ControllerContainer Controller { get; }

        /// <summary>
        /// Add the specified controller.
        /// </summary>
        /// <param name="controller"></param>
        void AddController(IController controller);

        /// <summary>
        /// Remove the specifed controller
        /// </summary>
        /// <param name="controller"></param>
        void RemoveController(IController controller);

        /// <summary>
        /// Find the specified controller. Returns null if not found.
        /// </summary>
        /// <param name="pControllerType"></param>
        /// <returns></returns>
        IController FindController(Type pControllerType);

        #endregion

        #region View

        /// <summary>
        /// Visual properties of this cell and other cell. You can share the VisualProperties between many cell to optimize memory size.
        /// Warning Changing this property can affect many cells
        /// </summary>
        IView View { get; set; }

        #endregion

        #region Model

        /// <summary>
        /// Model that contains the data of the cells. Cannot be null.
        /// </summary>
        ModelContainer Model { get; set; }

        #endregion

        #region Copy

        /// <summary>
        /// Create a shallow copy of the current object. Note that this is not a deep clone, all the reference are the same.
        /// Use internally MemberwiseClone method.
        /// </summary>
        /// <returns></returns>
        ICellVirtual Copy();

        #endregion
    }
}