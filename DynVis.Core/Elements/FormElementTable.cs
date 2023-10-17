using System;
using System.Windows.Forms;

namespace DynVis.Core.Elements
{
    /// <summary>
    /// Форма отображения таблицы Менделеева для выбора элемнта
    /// </summary>
    public partial class FormElementTable : Form
    {
        public FormElementTable()
        {
            InitializeComponent();
        }

        private void elementsTable_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public int ElementNumber
        {
            get { return elementsTable.ElementNumber; }
            set { elementsTable.ElementNumber = value; }
        }
    }
}
