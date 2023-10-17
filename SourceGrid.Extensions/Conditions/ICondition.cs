#region

using SourceGrid.Cells;

#endregion

namespace SourceGrid.Conditions
{
    public interface ICondition
    {
        bool Evaluate(DataGridColumn column, int gridRow, object itemRow);

        ICellVirtual ApplyCondition(ICellVirtual cell);
    }
}