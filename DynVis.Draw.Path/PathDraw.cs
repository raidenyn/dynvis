using System;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.ReactionPath;

namespace DynVis.Draw.Path
{
    public partial class PathDraw : BaseControl
    {
        public PathDraw()
        {
            InitializeComponent();
        }

        private PathTimer PathTimer;

        public IPath Path
        {
            get { return DrawEngine.Path; }
            set
            {
                if (Path != null)
                {
                    Path.CurrentPointChanged -= Path_CurrentPointChanged;

                    PathTimer = null;
                }
                
                DrawEngine.Path = value;

                if (Path != null)
                {
                    Path.CurrentPointChanged += Path_CurrentPointChanged;

                    PathTimer = new PathTimer(Path);
                }

                UpdateView();
            }
        }

        void UpdateView()
        {
            CurrentPointIndexChanged();
            Refresh();
        }

        void Path_CurrentPointChanged(object sender, EventArgs e)
        {
            drawBox.Refresh();
            UpdatetrackBarReactionCoordValue();
        }

        private readonly PathDrawEngine DrawEngine = new PathDrawEngine();

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawEngine.Draw(e.Graphics);
        }

        private void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            DrawEngine.SetSize(drawBox.Width, drawBox.Height);
        }

        private double Coeff
        {
            get
            {
                return (Path.Count - 1) / (double) (trackBarReactionCoord.Maximum - trackBarReactionCoord.Minimum);
            }
        }

        private void trackBarReactionCoord_Scroll(object sender, EventArgs e)
        {
            if (Path != null)
            {
                Path.CurrentPointIndex = (int) (Coeff*trackBarReactionCoord.Value);
            }
        }

        private void CurrentPointIndexChanged()
        {
            if (Path != null)
            {
                var newValue = (int) (Path.CurrentPointIndex/Coeff);

                if (newValue <= trackBarReactionCoord.Maximum && newValue >= trackBarReactionCoord.Minimum)
                {
                    if (newValue != trackBarReactionCoord.Value)
                        trackBarReactionCoord.Value = newValue;
                }
            }
        }

        private void UpdatetrackBarReactionCoordValue()
        {
            if (Path != null && Path.Count > 1)
            {
                trackBarReactionCoord.Value = (int) (Path.CurrentPointIndex/(double) (Path.Count - 1)*
                                                     (trackBarReactionCoord.Maximum -
                                                      trackBarReactionCoord.Minimum));

                if (trackBarReactionCoord.Value == trackBarReactionCoord.Maximum)
                {
                    SetStopMode();
                }
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            if (PathTimer != null)
            {
                if (PathTimer.IsMoving)
                {
                    PathTimer.StopMove();
                    SetStopMode();
                } 
                else
                {
                    PathTimer.StartMove();
                    SetMoveMode();
                }

            }
        }

        private void SetStopMode()
        {
            buttonMove.Text = "Показать анимацию";
        }

        private void SetMoveMode()
        {
            buttonMove.Text = "Остановить";
        }

        private void toolStripTextBoxStepCount_TextChanged(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(toolStripTextBoxStepCount.Text, out i) && i > 1)
            {
                PathTimer.StepCount = i;
            }
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            toolStripTextBoxStepCount.Visible = PathTimer != null;
            if (PathTimer != null)
            {
                toolStripTextBoxStepCount.Text = PathTimer.StepCount.ToString();
            }
        }

        private void buttonMenuConfig_Click(object sender, EventArgs e)
        {
            contextMenuStrip.Show(buttonMenuConfig, buttonMenuConfig.Width, 0);
        }
    }
}
