using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DynVis.Math;

namespace DynVis.Core.Geometry.Extension
{
    public partial class FormGeometryExtension : Form
    {
        public FormGeometryExtension()
        {
            InitializeComponent();
        }

        private IAtomStructure _AtomStructure;
        public IAtomStructure AtomStructure
        {
            get { return _AtomStructure; }
            set
            {
                if (AtomStructure != null) AtomStructure.GeometryChanged -= AtomStructure_GeometryChanged;
                _AtomStructure = value;
                if (AtomStructure != null) AtomStructure.GeometryChanged += AtomStructure_GeometryChanged;
            }
        }

        void AtomStructure_GeometryChanged(object sender, EventArgs e)
        {
            SetText();
        }

        private void FormGeometryExtension_Shown(object sender, EventArgs e)
        {
            
        }

        private void FormGeometryExtension_Activated(object sender, EventArgs e)
        {
            SetText();
        }

        private void SetText()
        {
            if (AtomStructure != null)
            {
                textBox.Text = "Квадрат матрицы координат:" + Environment.NewLine;
                textBox.Text += AtomStructure.ToPointEnumerable().GeometryMatrixSquare().ToMatrixString() + Environment.NewLine;

                double[] evals;
                double[,] evecs;

                AtomStructure.ToPointEnumerable().GeometryEigensystem(out evals, out evecs);

                textBox.Text += "Собственные значения:" + Environment.NewLine;
                textBox.Text += evals.ToColumnVaectorString() + Environment.NewLine;
                textBox.Text += "Собственные вектора:" + Environment.NewLine;
                textBox.Text += evecs.ToMatrixString() + Environment.NewLine;
            }
        }
    }
}
