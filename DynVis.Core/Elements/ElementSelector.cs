using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DynVis.Core.Elements
{
    /// <summary>
    /// Контрол позволяющий выбирать химический элемент
    /// </summary>
    public partial class ElementSelector : BaseControl
    {
        public ElementSelector()
        {
            InitializeComponent();

            ElementNumber = DefaultElement;
            UpdateElementView();
        }

        /// <summary>
        /// Элемент поумолчанию
        /// </summary>
        private const int DefaultElement = 1;

        private readonly FormElementTable FormElementTable = new FormElementTable();

        /// <summary>
        /// Подгоняем размер контрола по длину текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelElement_SizeChanged(object sender, EventArgs e)
        {
            Width = linkLabelElement.Width;
            Height = linkLabelElement.Height;
        }

        [DefaultValue(DefaultElement)]
        [Description("Номер химического элемента")]
        [Category("Chemical")]
        public int ElementNumber
        {
            get { return FormElementTable.ElementNumber; }
            set
            {
                FormElementTable.ElementNumber = value;
                UpdateElementView();
            }
        }

        private void linkLabelElement_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormElementTable.ShowDialog(this);
            UpdateElementView();
        }

        private void UpdateElementView()
        {
            linkLabelElement.Text = String.Format("{0}", Elements.GetElementSymbol(ElementNumber));
        }
    }
}
