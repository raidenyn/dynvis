#region

using SourceGrid.Cells;
using SourceGrid.Cells.Views;

#endregion

namespace SourceGrid.Conditions
{
    public class ConditionView : ICondition
    {
        #region Delegates

        public delegate bool EvaluateFunctionDelegate(DataGridColumn column, int gridRow, object itemRow);

        #endregion

        private readonly IView mView;
        public EvaluateFunctionDelegate EvaluateFunction;

        public ConditionView(IView view)
        {
            mView = view;
        }

        public IView View
        {
            get { return mView; }
        }

        #region ICondition Members

        public bool Evaluate(DataGridColumn column, int gridRow, object itemRow)
        {
            if (EvaluateFunction == null)
                return false;

            return EvaluateFunction(column, gridRow, itemRow);
        }

        public ICellVirtual ApplyCondition(ICellVirtual cell)
        {
            ICellVirtual copied = cell.Copy();
            copied.View = View;

            return copied;
        }

        #endregion
    }
}