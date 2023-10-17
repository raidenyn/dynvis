using System;
using System.ComponentModel;
using System.Drawing;
using DynVis.Core.Properties;
using SourceGrid;
using SourceGrid.Cells;
using ContentAlignment=DevAge.Drawing.ContentAlignment;

namespace DynVis.Core.Elements
{
    /// <summary>
    /// Контрол отображения таблицы Менделеева
    /// </summary>
    public partial class ElementsTable : BaseControl
    {
        public ElementsTable()
        {
            InitializeComponent();

            CreateTable();
        }

        /// <summary>
        /// Создает таблицу
        /// </summary>
        void CreateTable()
        {
            gridElements.Selection.Border.SetWidth(0);

            for (var number = 0; number < ElementInTable.ElementPosition.Length; number++)
            {
                gridElements[ElementInTable.ElementPosition[number].P.X, ElementInTable.ElementPosition[number].P.Y] =
                    CreateElementCell(number + 1, ElementInTable.ElementPosition[number].ElementType);
            }

            for (var number = 1; number < 12; number++)
            {
                gridElements[number, 0] = new Cell(number)
                                              {
                                                  View = {TextAlignment = ContentAlignment.MiddleCenter}
                                              };
            }

            gridElements[0, 1] = new Cell("I")
                                     {
                                         View = {TextAlignment = ContentAlignment.MiddleCenter}
                                     };
            gridElements[0, 2] = new Cell("II")
                                     {
                                         View = {TextAlignment = ContentAlignment.MiddleCenter}
                                     };
            gridElements[0, 3] = new Cell("III")
                                     {
                                         View = {TextAlignment = ContentAlignment.MiddleCenter}
                                     };
            gridElements[0, 4] = new Cell("IV")
                                     {
                                         View = {TextAlignment = ContentAlignment.MiddleCenter}
                                     };
            gridElements[0, 5] = new Cell("V")
                                     {
                                         View = {TextAlignment = ContentAlignment.MiddleCenter}
                                     };
            gridElements[0, 6] = new Cell("VI")
                                     {
                                         View = {TextAlignment = ContentAlignment.MiddleCenter}
                                     };
            gridElements[0, 7] = new Cell("VII")
                                     {
                                         View = {TextAlignment = ContentAlignment.MiddleCenter}
                                     };
            gridElements[0, 8] = new Cell("VIII")
                                     {
                                         View = {TextAlignment = ContentAlignment.MiddleCenter}
                                     };

            gridElements.Selection.EnableMultiSelection = false;
            gridElements.Selection.SelectionChanged += Selection_SelectionChanged;

            ElementNumber = 1;
        }

        /// <summary>
        /// Возращает выбраную в данный момент ячейку
        /// </summary>
        private ICell SelectedCell
        {
            get
            {
                var pos = gridElements.Selection.ActivePosition;
                return (pos.Column < 0 || pos.Row < 0) ? null : gridElements[pos.Row, pos.Column];
            }
        }

        private int _ElementNumber;

        [Description("Номер химического элемента")]
        [Category("Chemical")]
        public int ElementNumber
        {
            get
            {
                return _ElementNumber;
            }
            set
            {
                _ElementNumber = value;
                var pos = new Position(ElementInTable.ElementPosition[value].P.X,
                                       ElementInTable.ElementPosition[value].P.Y);
                gridElements.Selection.Focus(pos, true);
            }
        }

        void Selection_SelectionChanged(object sender, RangeRegionChangedEventArgs e)
        {
            if (SelectedCell != null && SelectedCell.Tag != null && SelectedCell.Tag.GetType() == typeof (int))
            {
                _ElementNumber = (int) SelectedCell.Tag;
            }
            else
            {
                _ElementNumber = 0;
            }


            labelSymbol.Text = Elements.GetElementSymbol(ElementNumber);
            labelNumber.Text = ElementNumber.ToString();
            labelName.Text = Elements.GetElementName(ElementNumber);
            labelMass.Text = String.Format(LangGeneral.MassOf, Elements.GetElementMass(ElementNumber));
        }

        static Cell CreateElementCell(int number, ElementType et)
        {
            var result = new Button(Elements.GetElementSymbol(number))
                             {
                                 Tag = number,
                                 View = new SourceGrid.Cells.Views.Button
                                            {
                                                TextAlignment = ContentAlignment.MiddleCenter,
                                                BackColor = Color.Gray
                                            },
                                 ToolTipText =
                                     String.Format(LangGeneral.NumberOf, Environment.NewLine,
                                                   Elements.GetElementName(number),
                                                   Elements.GetElementName(number), Elements.GetElementMass(number))
                             };

            switch (et)
            {
                case ElementType.S:
                    result.View.ForeColor = Color.Red;
                    break;
                case ElementType.P:
                    result.View.ForeColor = Color.Orange;
                    break;
                case ElementType.D:
                    result.View.ForeColor = Color.Blue;
                    break;
                case ElementType.F:
                    result.View.ForeColor = Color.Green;
                    break;
            }

             
            return result;
        }

        private void labelSymbol_SizeChanged(object sender, EventArgs e)
        {
            labelNumber.Left = labelSymbol.Left + labelSymbol.Width - 10;
        }

        private void gridElements_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClickButton != null) DoubleClickButton(this, e);
        }

        [Description("Событие возникает при двойном щелчке на элементе")]
        [Category("Chemical")]
        public event EventHandler DoubleClickButton;
    }


}
