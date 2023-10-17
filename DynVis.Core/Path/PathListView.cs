using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DynVis.Core.Properties;
using SourceGrid;
using SourceGrid.Cells;
using SourceGrid.Cells.Models;

namespace DynVis.Core.ReactionPath
{
    public partial class PathListView : UserControl
    {
        public PathListView()
        {
            InitializeComponent();

            CreateTable();
        }


        private const int RowHeaderCount = 1;
        private const int RowHeader = 0;

        private void CreateTable()
        {
            grid.ColumnsCount = Model.ColCount;
            grid.RowsCount = RowHeaderCount;

            grid[RowHeader, Model.ColNumber] = CreateHeader(LangGeneral.PathNamberColumnCaption);
            grid.Columns[Model.ColNumber].AutoSizeMode = SourceGrid.AutoSizeMode.MinimumSize;


            grid[RowHeader, Model.ColName] = CreateHeader(LangGeneral.PathNameColumnCaption);

            grid.Selection.FocusRowEntered += Selection_FocusRowEntered;
        }

        private void UpdateTable()
        {
            if (PathList != null)
            {
                grid.RowsCount = PathList.Count + RowHeaderCount;
                for (var row = 1; row <= PathList.Count; row++)
                {
                    grid[row, Model.ColNumber] = CreateHeader();
                    grid[row, Model.ColNumber].Tag = PathList[row-1];
                    

                    grid[row, Model.ColName] = CreateCell();
                }
            }
        }

        private IEnumerable<IPath> GetSelectedPaths()
        {
            var rows = grid.Selection.GetSelectionRegion().GetRowsIndex();
            var result = from row in rows
                    where grid[row, Model.ColNumber].Tag is IPath
                    select grid[row, Model.ColNumber].Tag as IPath;

            return result;
        }


        private IPath CurrentPath
        {
            get
            {
                var row = grid.Selection.ActivePosition.Row;
                if (row >=0) return grid[row, Model.ColNumber].Tag as IPath;

                return null;
            }
            set
            {
                var row = GetRow(value);
                if (row >= 0)
                {
                    grid.Selection.FocusRow(row);
                }
            }
        }

        private void Selection_FocusRowEntered(object sender, RowEventArgs e)
        {
            if (PathList != null)
            {
                if (CurrentPath != null)
                {
                    PathList.CurrentItem = CurrentPath;
                }
            }
        }

        private Header CreateHeader()
        {
            return new Header { Model = { ValueModel = ValueModel } };
        }

        private static Header CreateHeader(string text)
        {
            return new Header(text);
        }

        private Cell CreateCell()
        {
            var cell = new Cell { Model = { ValueModel = ValueModel }};
            cell.Editor = new SourceGrid.Cells.Editors.TextBox(typeof(string));
            return cell;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IPathList PathList
        {
            get
            {
                return ValueModel.PathLiat;
            }
            set
            {
                UnsetEvent();
                ValueModel.PathLiat = value;
                SetEvent();
                UpdateTable();
            }
        }

        private void UnsetEvent()
        {
            if (PathList != null)
            {
                PathList.CurrentItemChanged -= PathLiat_CurrentItemChanged;
                PathList.CountItemChanged -= PathLiat_CountItemChanged;
            }

        }

        private void SetEvent()
        {
            if (PathList != null)
            {
                PathList.CurrentItemChanged += PathLiat_CurrentItemChanged;
                PathList.CountItemChanged += PathLiat_CountItemChanged;
            }
        }

        void PathLiat_CountItemChanged(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private int GetRow(IPath path)
        {
            for (var row = 1; row < grid.RowsCount; row++)
            {
                if (grid[row, Model.ColNumber].Tag == path) return row;
            }
            return -1;
        }

        private void PathLiat_CurrentItemChanged(object sender, EventArgs e)
        {
            CurrentPath = PathList.CurrentItem;
        }

        private readonly Model ValueModel = new Model();

        private class Model : IValueModel
        {
            public IPathList PathLiat;

            public const int ColCount = 2;
            public const int ColNumber = 0;
            public const int ColName = 1;

            public object GetValue(CellContext cellContext)
            {
                switch (cellContext.Position.Column)
                {
                    case ColNumber:
                        return cellContext.Position.Row;
                    case ColName:
                        return PathLiat[cellContext.Position.Row - 1].Name;
                    default:
                        return null;
                }
            }

            public void SetValue(CellContext cellContext, object p_Value)
            {
                switch (cellContext.Position.Column)
                {
                    case ColName:
                        PathLiat[cellContext.Position.Row - 1].Name = p_Value.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ToolStripMenuItemDeletePath_Click(object sender, EventArgs e)
        {
            if (CurrentPath != null)
            {
                if (MessageBox.Show(this, LangGeneral.RemoveSelectedPaths, LangGeneral.ReactionPaths,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes)
                {
                    var paths = GetSelectedPaths().ToArray();
                    foreach (var path in paths)
                    {
                        PathList.Remove(path);
                    }
                }
            }
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            PathList.OpenPathDialog();
        }

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            PathList.SavePathDialog();
        }
    }
}
