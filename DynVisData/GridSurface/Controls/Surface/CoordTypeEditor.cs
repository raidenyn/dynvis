using System;
using DynVis.Core;
using DynVis.Core.Surface;

namespace DynVis.Data.GridSurface
{
    internal sealed partial class CoordTypeEditor : BaseControl
    {
        public CoordTypeEditor()
        {
            InitializeComponent();
        }

        private CoordinateType _CoordinateType;
        public CoordinateType CoordinateType
        {
            get
            {
                return _CoordinateType;
            }
            set
            {
                _CoordinateType = value;
                UpdateValue();
            }
        }

        private void radioButtonEnd_CheckedChanged(object sender, EventArgs e)
        {
            _CoordinateType = CoordinateType.Ended;
            RiseCoordTypeChanged();
        }
        private void radioButtonPeriodic_CheckedChanged(object sender, EventArgs e)
        {
            _CoordinateType = CoordinateType.Periodic;
            RiseCoordTypeChanged();
        }
        private void radioButtonMirrored_CheckedChanged(object sender, EventArgs e)
        {
            _CoordinateType = CoordinateType.Mirrored;
            RiseCoordTypeChanged();
        }

        private void UpdateValue()
        {
            switch (CoordinateType)
            {
                case CoordinateType.Ended:
                    radioButtonEnd.Checked = true;
                    break;
                case CoordinateType.Periodic:
                    radioButtonPeriodic.Checked = true;
                    break;
                case CoordinateType.Mirrored:
                    radioButtonMirrored.Checked = true;
                    break;
            }
            RiseCoordTypeChanged();
        }

        private void CoordTypeEditor_Load(object sender, EventArgs e)
        {
            UpdateValue();
        }

        public event EventHandler CoordTypeChanged;
        private void RiseCoordTypeChanged()
        {
            if (CoordTypeChanged != null) CoordTypeChanged(this, new EventArgs());
        }
    }
}
