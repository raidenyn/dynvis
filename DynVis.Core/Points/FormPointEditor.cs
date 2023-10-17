using System;
using System.Windows.Forms;

namespace DynVis.Core.Points
{
    public partial class FormPointEditor : Form
    {
        public FormPointEditor()
        {
            InitializeComponent();
        }

        private ISurfacePoint _SurfacePoint;
        public ISurfacePoint SurfacePoint
        {
            get
            {
                return _SurfacePoint;
            }
            set
            {
                _SurfacePoint = value;
                UpdateView();
            }
        }

        public bool ReadOnly
        {
            get
            {
                return ReadOnlyMode;
            }
            set
            {
                SetMode(value);
            }
        }

        private void UpdateView()
        {
            textBoxName.Text = SurfacePoint.Name;
            textBoxAddition.Text = SurfacePoint.AdditionalInformation;

            decimalEditorQ1.Value = SurfacePoint.Point.X;
            decimalEditorQ2.Value = SurfacePoint.Point.Y;

            SetMode(!(SurfacePoint is SurfacePoint));
        }

        private bool ReadOnlyMode;

        private void SetMode(bool readOnly)
        {
            textBoxName.ReadOnly =
                textBoxAddition.ReadOnly = decimalEditorQ1.ReadOnly = decimalEditorQ2.ReadOnly = readOnly;

            ReadOnlyMode = readOnly;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!ReadOnlyMode)
            {
                if (!decimalEditorQ1.IsValidValue) { decimalEditorQ1.Select(); return; }
                if (!decimalEditorQ2.IsValidValue) { decimalEditorQ2.Select(); return; }

                var surfacePoint = SurfacePoint as SurfacePoint;

                if (surfacePoint != null)
                {
                    SetValues(surfacePoint);

                    DialogResult = DialogResult.OK;
                    return;
                }
            }

            DialogResult = DialogResult.Cancel;
        }

        private void SetValues(SurfacePoint surfacePoint)
        {
            surfacePoint.Name = textBoxName.Text;
            surfacePoint.AdditionalInformation = textBoxAddition.Text;

            if (surfacePoint.PointD == null) surfacePoint.PointD = new Math.PointD();

            surfacePoint.PointD.X = decimalEditorQ1.Value;
            surfacePoint.PointD.Y = decimalEditorQ2.Value;
        }
    }
}
