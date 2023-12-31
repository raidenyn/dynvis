#region

using System;
using System.Collections.Generic;
using SourceGrid.Decorators;

#endregion

namespace SourceGrid.Selection
{
    public class RowSelection : SelectionBase
    {
        private readonly List<int> mList = new List<int>();
        private DecoratorSelection mDecorator;

        public override void BindToGrid(GridVirtual p_grid)
        {
            base.BindToGrid(p_grid);

            mDecorator = new DecoratorSelection(this);
            Grid.Decorators.Add(mDecorator);
        }

        public override void UnBindToGrid()
        {
            Grid.Decorators.Remove(mDecorator);

            base.UnBindToGrid();
        }

        public override bool IsSelectedColumn(int column)
        {
            return false;
        }

        public override void SelectColumn(int column, bool select)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool IsSelectedRow(int row)
        {
            return mList.Contains(row);
        }

        public override void SelectRow(int row, bool select)
        {
            if (select && mList.Contains(row) == false)
            {
                mList.Add(row);

                OnSelectionChanged(new RangeRegionChangedEventArgs(Grid.Rows.GetRange(row), Range.Empty));
            }
            else if (!select && mList.Contains(row))
            {
                mList.Remove(row);

                OnSelectionChanged(new RangeRegionChangedEventArgs(Range.Empty, Grid.Rows.GetRange(row)));
            }
        }

        public override bool IsSelectedCell(Position position)
        {
            return IsSelectedRow(position.Row);
        }

        public override void SelectCell(Position position, bool select)
        {
            SelectRow(position.Row, select);
        }

        public override bool IsSelectedRange(Range range)
        {
            for (int r = range.Start.Row; r <= range.End.Row; r++)
            {
                if (IsSelectedRow(r) == false)
                    return false;
            }

            return true;
        }

        public override void SelectRange(Range range, bool select)
        {
            for (int r = range.Start.Row; r <= range.End.Row; r++)
            {
                SelectRow(r, select);
            }
        }

        protected override void OnResetSelection()
        {
            RangeRegion prevRange = GetSelectionRegion();

            mList.Clear();

            OnSelectionChanged(new RangeRegionChangedEventArgs(null, prevRange));
        }

        public override bool IsEmpty()
        {
            return mList.Count == 0;
        }

        public override RangeRegion GetSelectionRegion()
        {
            var region = new RangeRegion();

            if (Grid.Columns.Count > 0)
            {
                foreach (int row in mList)
                {
                    region.Add(ValidateRange(new Range(row, 0, row, Grid.Columns.Count - 1)));
                }
            }

            return region;
        }

        public override bool IntersectsWith(Range rng)
        {
            for (int r = rng.Start.Row; r <= rng.End.Row; r++)
            {
                if (IsSelectedRow(r))
                    return true;
            }

            return false;
        }
    }
}