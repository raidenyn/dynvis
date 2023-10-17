#region

using System.Drawing;
using SourceGrid.Cells.Views;

#endregion

namespace SourceGrid.Conditions
{
    public static class ConditionBuilder
    {
        public static ICondition AlternateView(
            IView view,
            Color alternateBackcolor,
            Color alternateForecolor)
        {
            var viewAlternate = (IView) view.Clone();
            viewAlternate.BackColor = alternateBackcolor;
            viewAlternate.ForeColor = alternateForecolor;

            var condition =
                new ConditionView(viewAlternate);

            condition.EvaluateFunction =
                delegate(DataGridColumn column, int gridRow, object itemRow) { return (gridRow & 1) == 1; };

            return condition;
        }
    }
}