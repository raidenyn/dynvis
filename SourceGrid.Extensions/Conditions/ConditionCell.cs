#region

using SourceGrid.Cells;

#endregion

namespace SourceGrid.Conditions
{
    public class ConditionCell : ICondition
    {
        #region Delegates

        public delegate bool EvaluateFunctionDelegate(DataGridColumn column, int gridRow, object itemRow);

        #endregion

        private readonly ICellVirtual mCell;
        public EvaluateFunctionDelegate EvaluateFunction;

        public ConditionCell(ICellVirtual cell)
        {
            mCell = cell;
        }

        public ICellVirtual Cell
        {
            get { return mCell; }
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
            return Cell;
        }

        #endregion
    }
}