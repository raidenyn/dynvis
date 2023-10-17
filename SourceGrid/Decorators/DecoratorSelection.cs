#region

using System.Drawing;
using SourceGrid.Selection;

#endregion

namespace SourceGrid.Decorators
{
    public class DecoratorSelection : DecoratorBase
    {
        private readonly SelectionBase mSelection;

        public DecoratorSelection(SelectionBase selection)
        {
            mSelection = selection;
        }

        public override bool IntersectWith(Range range)
        {
            return mSelection.IntersectsWith(range);
        }

        public override void Draw(RangePaintEventArgs e)
        {
            RangeRegion region = mSelection.GetSelectionRegion();

            if (region.IsEmpty())
                return;

            Brush brush = e.GraphicsCache.BrushsCache.GetBrush(mSelection.BackColor);

            var focusContext = new CellContext(e.Grid, mSelection.ActivePosition);
            Rectangle focusRect = e.Grid.PositionToRectangle(mSelection.ActivePosition);

            //Draw each selection range
            foreach (Range rng in region)
            {
                Rectangle rectToDraw = e.Grid.RangeToRectangle(rng);
                if (rectToDraw == Rectangle.Empty)
                    continue;

                var regionToDraw = new Region(rectToDraw);

                if (rectToDraw.IntersectsWith(focusRect))
                    regionToDraw.Exclude(focusRect);

                e.GraphicsCache.Graphics.FillRegion(brush, regionToDraw);

                //Draw the border only if there isn't a editing cell
                // and is the range that contains the focus or there is a single range
                if (rng.Contains(mSelection.ActivePosition) || region.Count == 1)
                {
                    if (focusContext == null || focusContext.IsEditing() == false)
                        mSelection.Border.Draw(e.GraphicsCache, rectToDraw);
                }
            }

            //Draw Focus
            Brush brushFocus = e.GraphicsCache.BrushsCache.GetBrush(mSelection.FocusBackColor);
            e.GraphicsCache.Graphics.FillRectangle(brushFocus, focusRect);
        }
    }
}